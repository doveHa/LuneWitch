using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ToonyVoices))]
public class Demo : MonoBehaviour
{
    //--------------------------------------------------------------------------
    #region Properties

    [SerializeField]
    private Text[] _textFields;
    [SerializeField]
    private Image[] _textBubbles;
    private ToonyVoices _voices;
    private int _currentIndex = 0;
    private string[] _messages = {
        "ToonyVoice Test 안녕하세요 반갑습니다",
        "한글 사용 적용, 캐릭터 별 음성 달라짐 확인 중",
        "반갑습니다"
    };

    #endregion

    //--------------------------------------------------------------------------
    #region Unity methods

    private void Start()
    {
        foreach(Image image in _textBubbles) { image.gameObject.SetActive(false); }
        foreach(Text text in _textFields) { text.gameObject.SetActive(false); }
        _voices = GetComponent<ToonyVoices>();

        StartCoroutine(DelaySpeech(0.5f));
    }

    #endregion

    //--------------------------------------------------------------------------
    #region Class methods

    private IEnumerator DelaySpeech(float delay)
    {
        yield return new WaitForSeconds(delay);
        _textFields[_currentIndex].gameObject.SetActive(true);
        _textBubbles[_currentIndex].gameObject.SetActive(true);
        if(_currentIndex == 1)
        {
            _voices.Speak(_messages[1], 3f);
            yield break;
        }
        if(_currentIndex == 2)
        {
            _voices.Speak(_messages[2], 2f, 0.7f);
            yield break;
        }
        _voices.Speak(_messages[_currentIndex]);
    }

    private IEnumerator CloseLastBubble()
    {
        yield return new WaitForSeconds(0.5f);
        _textFields[_currentIndex - 1].gameObject.SetActive(false);
        _textBubbles[_currentIndex - 1].gameObject.SetActive(false);
    }

    #endregion

    //--------------------------------------------------------------------------
    #region Class event handlers

    public void OnCharacterSounded(string character)
    {
        _textFields[_currentIndex].text += character;
    }

    public void OnSentenceFinished()
    {
        _currentIndex++;
        if(_currentIndex == _messages.Length)
        {
            StartCoroutine(CloseLastBubble());
            return;
        }
        foreach(Image image in _textBubbles) { image.gameObject.SetActive(false); }
        foreach(Text text in _textFields) { text.gameObject.SetActive(false); }
        StartCoroutine(DelaySpeech(0.25f));
    }

    #endregion
}
