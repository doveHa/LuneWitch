using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    //public static SoundManager Instance;

    public AudioClip[] sfxClips;
    public int poolSize = 10;
    public AudioMixerGroup sfxMixerGroup;

    private AudioSource[] sfxSources;
    private int currentIndex = 0;

    // 너무 자주 재생되면 안 되는 효과음의 제한 시간
    private float[] lastPlayedTimes;
    private float minInterval = 0.1f; // 예: 0.1초 간격 제한

    // 자주 눌러도 되는 효과음 인덱스
    public int[] allowRapidRepeatIndices;

    void Awake()
    {
        /*
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }
        
        */
        // AudioSource 풀 생성
        sfxSources = new AudioSource[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = new GameObject("SFX Source " + i);
            obj.transform.parent = transform;

            var audio = obj.AddComponent<AudioSource>();
            audio.playOnAwake = false;
            audio.outputAudioMixerGroup = sfxMixerGroup;
            audio.volume = 0.5f; // 🔊 기본 볼륨을 50%로 설정

            sfxSources[i] = audio;
        }

        // 시간 제한 배열 초기화
        lastPlayedTimes = new float[sfxClips.Length];
    }

    public void PlaySFX(int index)
    {
        if (index < 0 || index >= sfxClips.Length) return;
        if (sfxClips[index] == null) return; // 🎯 null AudioClip 방지

        bool allowRapid = System.Array.Exists(allowRapidRepeatIndices, i => i == index);

        // 빠른 반복 허용 안 되면 시간 체크
        if (!allowRapid && Time.time - lastPlayedTimes[index] < minInterval)
            return;

        // PlayOneShot은 클립이 겹쳐도 자연스럽게 들림
        sfxSources[currentIndex].PlayOneShot(sfxClips[index]);

        // 시간 갱신
        lastPlayedTimes[index] = Time.time;
        currentIndex = (currentIndex + 1) % poolSize;
    }
}