using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum SpeakerType { None, Left, Right }

[System.Serializable]
public class DialogueLine
{
    [Tooltip("말하는 캐릭터 선택")]
    public SpeakerType speakerType;

    [Tooltip("캐릭터 이름")]
    public string speakerName;

    [Tooltip("대사 내용")]
    [TextArea(3, 5)]
    public string text;

    [Tooltip("왼쪽 캐릭터 스프라이트")]
    public Sprite leftSprite;

    [Tooltip("오른쪽 캐릭터 스프라이트")]
    public Sprite rightSprite;

    [Tooltip("스토리 연출 명령어")]
    public string eventCommand;
}

[CreateAssetMenu(fileName = "New Story", menuName = "Dialogue/Story Data")]
public class DialogueData : ScriptableObject
{
    [Header("Intro")]
    public string introTitle;
    [TextArea] public string introDescription;

    [Header("Background")]
    public Sprite backgroundSprite;
    public bool isIllustrationMode; // 일러스트 모드면 캐릭터 이미지X
    public Sprite illustrationSprite;

    [Header("TSV Text File")]
    public TextAsset tsvFile;

    [Header("Dialogue Lines")]
    public List<DialogueLine> lines;

    [Header("Outro")]
    public bool isChapterEnd; // 챕터 종료 여부 -> true면 보상 패널
    [TextArea]
    public string chapterClearMessage;
    public int nextBattleRoundNo = 0;
    public string nextSceneName; // 다음 장면 이름, BattleScene or Main
    public DialogueData nextStory;

    public DialogueData nextStoryAfterBattle;

    // 대본 데이터를 TSV 파일에서 불러오는 메서드
#if UNITY_EDITOR
    [ContextMenu("TSV 데이터 불러오기")] // 메뉴 이름 변경
    public void LoadDialogueFromTSV()
    {
        if (tsvFile == null)
        {
            Debug.LogError("텍스트(.txt) 파일을 먼저 할당해주세요!");
            return;
        }

        lines = new List<DialogueLine>();

        // 엔터(\n) 기준으로 줄 나누기
        string[] rows = tsvFile.text.Split('\n');

        for (int i = 0; i < rows.Length; i++) //첫 줄부터 시작
        {
            string row = rows[i];
            if (string.IsNullOrWhiteSpace(row)) continue;

            string[] data = row.Split('\t');

            if (data.Length < 3) continue;

            DialogueLine newLine = new DialogueLine();

            // 1. 화자 타입
            string typeStr = data[0].Trim();
            if (System.Enum.TryParse(typeStr, true, out SpeakerType type))
                newLine.speakerType = type;
            else
                newLine.speakerType = SpeakerType.None;

            // 2. 이름
            newLine.speakerName = data[1].Trim();

            string rawText = data[2].Trim();

            if (rawText.StartsWith("\"") && rawText.EndsWith("\""))
            {
                rawText = rawText.Substring(1, rawText.Length - 2);
            }

            newLine.text = rawText.Replace("\"\"", "\"");

            // 4. 이미지 찾기
            if (data.Length > 3) newLine.leftSprite = FindSprite(data[3].Trim());
            if (data.Length > 4) newLine.rightSprite = FindSprite(data[4].Trim());

            // 이벤트 커맨드
            if (data.Length > 5)
            {
                newLine.eventCommand = data[5].Trim();
            }

            lines.Add(newLine);
        }

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        Debug.Log("TSV 불러오기 완료! 총 대사 수: " + lines.Count);
    }

    private Sprite FindSprite(string spriteName)
    {
        if (string.IsNullOrEmpty(spriteName)) return null;
        string[] guids = AssetDatabase.FindAssets(spriteName + " t:Sprite");
        if (guids.Length > 0)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            return AssetDatabase.LoadAssetAtPath<Sprite>(path);
        }
        return null;
    }
#endif
}