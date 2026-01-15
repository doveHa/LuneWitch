using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using Script.Manager;

public class SoundManager : MonoBehaviour
{
    //public static SoundManager Instance;
    /*
     * HR: 싱글톤을 쓰지 않는 이유는 PlayerPrefs를 이용해
     * 하드 디스크에 BGM 및 SFX 볼륨 값을 저장하기 때문임.
     * 싱글톤을 사용하니 Story 씬에서 에러 발생
     * VolumeSettingHandler.cs 참고
     */

    [Header("BGM Settings")]
    public AudioSource bgmSource;
    public AudioClip chapter1BGM;
    public AudioClip chapter2BGM;
    public AudioClip bossBGM;
    public AudioClip infinityBGM;
    // 브금을 대체 어디서 관리하는지 모르겠음

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

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "BattleScene")
        {
            PlayBattleBGM();
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource == null || clip == null) return;

        // 이미 같은 음악이 나오고 있다면 다시 틀지 않음 (끊김 방지)
        if (bgmSource.clip == clip && bgmSource.isPlaying) return;

        bgmSource.Stop();
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    private void PlayBattleBGM()
    {
        AudioClip clipToPlay = null;

        // 1. 무한 모드인지 먼저 확인
        if (SceneLoadManager.isInfinityMode)
        {
            clipToPlay = infinityBGM;
        }
        else
        {
            // 2. 챕터별 분기
            switch (SceneLoadManager.SelectedChapterNo)
            {
                case 1:
                    // 챕터 1은 무조건 이 음악
                    clipToPlay = chapter1BGM;
                    break;

                case 2:
                    // 챕터 2는 라운드에 따라 다름
                    if (SceneLoadManager.SelectedRoundNo == 3)
                    {
                        // 2-3은 보스전
                        clipToPlay = bossBGM;
                    }
                    else
                    {
                        // 2-1, 2-2는 일반 전투
                        clipToPlay = chapter2BGM;
                    }
                    break;

                default:
                    // 예외 상황 (혹시 챕터3이 생기면 여기서 처리하거나 기본값 설정)
                    clipToPlay = chapter1BGM;
                    break;
            }
        }

        // 최종 결정된 음악 재생
        PlayBGM(clipToPlay);
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