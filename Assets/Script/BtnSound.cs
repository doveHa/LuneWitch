using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BtnSound : MonoBehaviour
{
    public int sfxIndex = 0; // 보통 0이 버튼 클릭 사운드임

    private Button btn;
    private SoundManager soundManager;

    void Start()
    {
        btn = GetComponent<Button>();

        soundManager = FindObjectOfType<SoundManager>();

        if (soundManager == null)
        {
            Debug.LogWarning("씬에 SoundManager가 없음");
            return;
        }

        btn.onClick.AddListener(() => soundManager.PlaySFX(sfxIndex));
    }
}
