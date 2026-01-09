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

    [Header("TSV Text File")]
    public TextAsset tsvFile;

    [Header("Dialogue Lines")]
    public List<DialogueLine> lines;

    [Header("Outro")]
    public bool isChapterEnd; // 챕터 종료 여부 -> true면 보상 패널
    public string nextSceneName; // 다음 장면 이름, BattleScene or Main

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

        // 첫 줄(헤더) 건너뛰고 1부터 시작
        for (int i = 1; i < rows.Length; i++)
        {
            string row = rows[i];
            if (string.IsNullOrWhiteSpace(row)) continue;

            // 핵심 변경점: 쉼표(',')가 아니라 탭('\t')으로 나눕니다.
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

            // 3. 대사 (이제 쉼표를 마음껏 써도 됩니다! Replace 필요 없음)
            // 엑셀에서 줄바꿈(Alt+Enter)을 썼다면 따옴표가 생길 수 있어 제거해줍니다.
            string rawText = data[2].Trim();
            // 엑셀 특성상 셀 안에 줄바꿈이 있으면 앞뒤로 따옴표(")가 붙는데 그거 제거
            if (rawText.StartsWith("\"") && rawText.EndsWith("\""))
            {
                rawText = rawText.Substring(1, rawText.Length - 2);
            }
            // 엑셀 줄바꿈은 "" 두개로 표현되므로 하나로 변경
            newLine.text = rawText.Replace("\"\"", "\"");

            // 4. 이미지 찾기
            if (data.Length > 3) newLine.leftSprite = FindSprite(data[3].Trim());
            if (data.Length > 4) newLine.rightSprite = FindSprite(data[4].Trim());

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