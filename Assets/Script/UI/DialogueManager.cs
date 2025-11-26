using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum  SpeakerType
{
    None,
    Left,
    Right
}

[System.Serializable]
public struct  Speaker
{
    public SpriteRenderer spriteRender;
    public Image imageDialog;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDialog;
    public GameObject NextBtn;
}

[System.Serializable]
public struct DialogueData
{
    public SpeakerType speakerType; //말하는 캐릭터
    public string speakerName; //말하는 캐릭터 이름
    [TextArea(3,10)]
    public string[] sentences; // 대사
    public Sprite leftSprite; //왼쪽 캐릭터 스프라이트
    public Sprite rightSprite; //오른쪽 캐릭터 스프라이트
}

public class DialogueManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}