using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using Script.DataDefinitions.ScriptableObjects;

public class GachaEffectHandler : MonoBehaviour
{
    [Header("단일 결과 UI")] public GameObject gachaPanel;
    public GameObject backgroundEffect;
    public Image characterImage;
    public TMP_Text characterNameText;
    public Animator characterAnimator;

    [Header("다중 결과 UI")] public GameObject multiResultPanel;
    public List<GachaResultSlot> resultSlots; // 슬롯 5개 등록

    private bool isWaitingForClick = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isWaitingForClick)
            {
                ClosePanel();
            }
            else if (multiResultPanel.activeSelf)
            {
                CloseMultiResultPanel();
            }
        }
    }

    public void PlayGachaEffect(CharacterData characterData)
    {
        StartCoroutine(PlaySequence(characterData));
    }

    private IEnumerator PlaySequence(CharacterData characterData)
    {
        gachaPanel.SetActive(true);

        if (backgroundEffect != null)
            backgroundEffect.SetActive(true);

        if (characterAnimator != null)
        {
            characterAnimator.gameObject.SetActive(true);
            characterAnimator.Play("Appear 1");
        }

        characterImage.sprite = characterData.characterImage;
        characterNameText.text = characterData.characterName;


        isWaitingForClick = true;
        yield return null;
    }

    private void ClosePanel()
    {
        gachaPanel.SetActive(false);
        isWaitingForClick = false;

        if (backgroundEffect != null)
            backgroundEffect.SetActive(false);
    }

    public void ShowMultipleResults(List<CharacterData> characters)
    {
        multiResultPanel.SetActive(true);

        for (int i = 0; i < resultSlots.Count; i++)
        {
            if (i < characters.Count)
            {
                resultSlots[i].SetData(characters[i]);
            }
            else
            {
                resultSlots[i].Clear();
            }
        }
    }

    public void CloseMultiResultPanel()
    {
        multiResultPanel.SetActive(false);
    }
}