using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

static public class ToonyVoicesResources
{
	//--------------------------------------------------------------------------
	#region Fields

	static private Dictionary<string, AudioClip> _sounds = new Dictionary<string, AudioClip>();

    #endregion

    //--------------------------------------------------------------------------
    #region Class methods

    /// <summary>
    /// Attempts to return an AudioClip for a given letter sound.
    /// <see cref="PopulateSoundDictionary"/> called to ensure dictionary is not empty.
    /// </summary>
    /// <param name="forSound">Character representing the sound</param>
    /// <returns>AudioClip or null if sound not found</returns>
    static public AudioClip GetAudioClip(string forSound)
	{
		PopulateSoundDictionary();
		return _sounds[forSound] ?? null;
	}

	/// <summary>
	/// Checks if the key exists within the dictionary.
	/// <see cref="PopulateSoundDictionary"/> called to ensure dictionary is not empty.
	/// </summary>
	/// <param name="key">Key value as a string to search for</param>
	/// <returns>A boolean value based on the existence of the key</returns>
	static public bool ContainsKey(string key)
    {
		PopulateSoundDictionary();
		return _sounds.ContainsKey(key);
    }

	/// <summary>
	/// Creates a dictionary of letter sounds associated to AudioClips.
	/// Does nothing is the dictionary is already populated.
	/// </summary>
	static private void PopulateSoundDictionary()
	{
		if(_sounds.Count > 0) { return; }

		_sounds.Add("a", Resources.Load("a") as AudioClip);
		_sounds.Add("b", Resources.Load("b") as AudioClip);
		_sounds.Add("c", Resources.Load("c") as AudioClip);
		_sounds.Add("d", Resources.Load("d") as AudioClip);
		_sounds.Add("e", Resources.Load("e") as AudioClip);
		_sounds.Add("f", Resources.Load("f") as AudioClip);
		_sounds.Add("g", Resources.Load("g") as AudioClip);
		_sounds.Add("h", Resources.Load("h") as AudioClip);
		_sounds.Add("i", Resources.Load("i") as AudioClip);
		_sounds.Add("j", Resources.Load("j") as AudioClip);
		_sounds.Add("k", Resources.Load("k") as AudioClip);
		_sounds.Add("l", Resources.Load("l") as AudioClip);
		_sounds.Add("m", Resources.Load("m") as AudioClip);
		_sounds.Add("n", Resources.Load("n") as AudioClip);
		_sounds.Add("o", Resources.Load("o") as AudioClip);
		_sounds.Add("p", Resources.Load("p") as AudioClip);
		_sounds.Add("q", Resources.Load("q") as AudioClip);
		_sounds.Add("r", Resources.Load("r") as AudioClip);
		_sounds.Add("s", Resources.Load("s") as AudioClip);
		_sounds.Add("t", Resources.Load("t") as AudioClip);
		_sounds.Add("u", Resources.Load("u") as AudioClip);
		_sounds.Add("v", Resources.Load("v") as AudioClip);
		_sounds.Add("w", Resources.Load("w") as AudioClip);
		_sounds.Add("x", Resources.Load("x") as AudioClip);
		_sounds.Add("y", Resources.Load("y") as AudioClip);
		_sounds.Add("z", Resources.Load("z") as AudioClip);
		_sounds.Add("th", Resources.Load("th") as AudioClip);
		_sounds.Add("sh", Resources.Load("sh") as AudioClip);
		_sounds.Add(" ", Resources.Load("pause") as AudioClip);
		_sounds.Add(".", Resources.Load("pauselong") as AudioClip);
	}

	#endregion
}

[Serializable]
public class CharacterSoundedEvent : UnityEvent<string> { }

[RequireComponent(typeof(AudioSource))]
public class ToonyVoices : MonoBehaviour
{
    //--------------------------------------------------------------------------
    #region Class structs

    private struct CharacterToken
    {
        //--------------------------------------------------------------------------
        #region Properties

        public string Character { get; private set; }
		public bool Inflective { get; private set; }

        #endregion

        //--------------------------------------------------------------------------
        #region Constructor

        public CharacterToken(string character, bool inflective)
        {
			Character = character;
			Inflective = inflective;
        }

        #endregion
    }

	#endregion

	//--------------------------------------------------------------------------
	#region Fields

	[SerializeField]
	private float _basePitch = 2f;
	[SerializeField]
	private float _pitchRange = 0.35f;
	[SerializeField]
	private float _inflectionPitchModifier = 0.4f;
    [SerializeField]
	private CharacterSoundedEvent _characterSoundedEvent = null;
	[SerializeField]
	private UnityEvent _sentenceFinishedEvent = null;
	private AudioSource _source;
	private Queue<CharacterToken> _queue = new Queue<CharacterToken>();
	private float _previousVolume;

	//HH: 코루틴 제어 변수 추가
	private Coroutine _speechCoroutine;

    #endregion

    //--------------------------------------------------------------------------
    #region Properties

	public CharacterSoundedEvent CharacterSounded
    {
		get
        {
			return _characterSoundedEvent;
        }
    }

	public UnityEvent SentenceFinished
    {
		get
        {
			return _sentenceFinishedEvent;
        }
    }

    #endregion

    //--------------------------------------------------------------------------
    #region Unity methods

    private void Awake() //Start -> Awake
    {
		_source = GetComponent<AudioSource>();
		_source.playOnAwake = false;
		_source.pitch = _basePitch;
		_previousVolume = _source.volume;
	}

    #endregion

    #region Hangul Extension

	// 초성 매핑 테이블
	private readonly string[] _choSungTable =
        { "g", "k", "n", "d", "t", "l", "m", "b", "p", "s", "s", "a", "j", "j", "c", "k", "t", "p", "h" };

	// Speak 메서드 한글 변환 로직 적용
	public void Speak(string sentence)
	{
		StopSpeech();

		string convertedSentence = ConvertHangulToRoman(sentence);

		Process(convertedSentence);
		PlayNextSound(_basePitch);
    }

    // 오버로딩 메서드
    public void Speak(string sentence, float pitch, float volume = 1f)
    {
        StopSpeech();
        _previousVolume = _source.volume;
        _source.volume = volume;

        string convertedSentence = ConvertHangulToRoman(sentence);
        Process(convertedSentence);
        PlayNextSound(pitch);
    }

    // 소리 즉시 중지 (skip, next)
    public void StopSpeech()
    {
        _queue.Clear();
        if (_source.isPlaying) _source.Stop();
        if (_speechCoroutine != null) StopCoroutine(_speechCoroutine);
    }

    // 한글 알파벳 소리로 변환
    private string ConvertHangulToRoman(string input)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        foreach (char c in input)
        {
            if (c >= 0xAC00 && c <= 0xD7A3) // 한글 유니코드 범위
            {
                // 초성 인덱스 추출: (문자 - 0xAC00) / 28 / 21
                int choSungIndex = (c - 0xAC00) / 28 / 21;
                sb.Append(_choSungTable[choSungIndex]);
            }
            else
            {
                // 한글이 아니면 그대로 유지 (영어, 공백, 특수문자 등)
                sb.Append(c);
            }
        }
        return sb.ToString();
    }

    #endregion


    //--------------------------------------------------------------------------
    #region Class methods

    /// <summary>
    /// Using <see cref="Process(string)"/>, processes the sentence to be spoken.
    /// Calls <see cref="PlayNextSound"/> for each sound in queue.
    /// </summary>
    /// <param name="sentence">The sentence to be spoken</param>
/*    public void Speak(string sentence)
    {
		Process(sentence);
		PlayNextSound(_basePitch);
    }*/

	/// <summary>
	/// Using <see cref="Process(string)"/>, processes the sentence to be spoken.
	/// Calls <see cref="PlayNextSound"/> for each sound in queue.
	/// </summary>
	/// <param name="sentence">The sentence to be spoken</param>
	/// <param name="pitch">The pitch for this sentence, reverts back to the base pitch when finished</param>
	/// <param name="volume">The volume for this sentence to be spoken</param>
/*	public void Speak(string sentence, float pitch, float volume = 1f)
	{
		_previousVolume = _source.volume;
		_source.volume = volume;
		Process(sentence);
		PlayNextSound(pitch);
	}*/

	/// <summary>
	/// Processes the input string, splitting by spaces, and calls <see cref="ParseWord(string)"/> for each word individually.
	/// Clears the queue of any remaining tokens.
	/// </summary>
	/// <param name="input">Input string to be processed</param>
	private void Process(string input)
    {
		_queue.Clear();
		foreach(string word in input.Split(' '))
        {
			ParseWord(word);
			AddToQueue(" ", false);
        }
    }

	/// <summary>
    /// Breaks the word down character by character, looking for inflection, pauses, and compound sounds ('th', 'sh').
    /// <see cref="AddToQueue(string, bool)"/> called for each character found.
    /// </summary>
    /// <param name="word">Word to be parsed</param>
	private void ParseWord(string word)
    {
        if (string.IsNullOrEmpty(word)) // 단어가 비어 있는 경우 계산 X, 무시
        {
            return;
        }

        bool skipNextCharacter = false;
		bool inflective = (word[word.Length - 1] == '?');

		for(int i = 0; i < word.Length; i++)
        {
			string charString = word[i].ToString();
			if (skipNextCharacter == true)
			{
				skipNextCharacter = false;
				continue;
			}

			if(i < word.Length - 1)
            {
				string substring = word.Substring(i, 2);
				if(ToonyVoicesResources.ContainsKey(substring.ToLower()) == true)
                {
					AddToQueue(substring, inflective);
					skipNextCharacter = true;
					continue;
                }
            }

			AddToQueue(charString, inflective);
        }
    }

	/// <summary>
    /// Adds the specified character into the queue to be spoken.
    /// </summary>
    /// <param name="character">Character to enqueue</param>
    /// <param name="inflective">Is the sentence inflective</param>
	private void AddToQueue(string character, bool inflective)
    {
		CharacterToken symbol = new CharacterToken(character, inflective);
		_queue.Enqueue(symbol);
    }

	/// <summary>
    /// Plays the next sound in queue, fires sounded and finished events, calls <see cref="PlayNextSound"/> until queue is empty.
    /// </summary>
	private void PlayNextSound(float pitch)
    {
		if(_queue.Count == 0)
        {
			_source.volume = _previousVolume;
			if(_sentenceFinishedEvent != null)
            {
				_sentenceFinishedEvent.Invoke();
            }
			return;
        }

		CharacterToken token = _queue.Dequeue();
		if (_characterSoundedEvent != null) { _characterSoundedEvent.Invoke(token.Character); }
		if (ToonyVoicesResources.ContainsKey(token.Character) == false)
        {
			PlayNextSound(pitch);
			return;
        }

		AudioClip clip = ToonyVoicesResources.GetAudioClip(token.Character);
		if(clip == null)
        {
			PlayNextSound(pitch);
			return;
        }

		_source.clip = clip;
		_source.pitch = pitch +
						UnityEngine.Random.Range(-_pitchRange, _pitchRange) +
						((token.Inflective == true) ? _inflectionPitchModifier : 0f);
		_source.Play();

		//StartCoroutine(WaitForAudioCompleted(pitch));
		_speechCoroutine = StartCoroutine(WaitForAudioCompleted(pitch));
    }

	/// <summary>
    /// Waits for <see cref="AudioSource.isPlaying"/> to return false before playing next sound
    /// </summary>
    /// <param name="pitch">The pitch for the next sound to be played at</param>
    /// <returns></returns>
	private IEnumerator WaitForAudioCompleted(float pitch)
    {
		while(_source.isPlaying == true)
        {
			yield return null;
        }
		PlayNextSound(pitch);
    }

    #endregion
}
