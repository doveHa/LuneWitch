using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettingHandler : MonoBehaviour
{
    [SerializeReference] private AudioMixer myMixer;
    [SerializeReference] private Slider BGMSlider;
    [SerializeReference] private Slider sfxSlider;

    public void Start()
    {
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetBGMVolume();
            SetsfxVolume();
        }
    }

    public void SetBGMVolume()
    {
        float volume = BGMSlider.value;
        myMixer.SetFloat("BGM", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }
    public void SetsfxVolume()
    {
        float volume = sfxSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void LoadVolume()
    {
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        SetBGMVolume();
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SetsfxVolume();
    }
}
