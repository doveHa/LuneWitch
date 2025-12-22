using UnityEngine;
using System.Collections.Generic;

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

    [Tooltip("왼쪽 캐릭터 스프라이트 (비워두면 기존 유지/숨김 처리 가능)")]
    public Sprite leftSprite;

    [Tooltip("오른쪽 캐릭터 스프라이트")]
    public Sprite rightSprite;
}

[CreateAssetMenu(fileName = "New Story", menuName = "Dialogue/Story Data")]
public class DialogueData : ScriptableObject
{
    public List<DialogueLine> lines; // 대사 리스트
}