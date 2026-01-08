using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;

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

    [Header("Dialogue Lines")]
    public List<DialogueLine> lines;

    [Header("Outro")]
    public bool isChapterEnd; // 챕터 종료 여부 -> true면 보상 패널
    public string nextSceneName; // 다음 장면 이름, BattleScene or Main
}