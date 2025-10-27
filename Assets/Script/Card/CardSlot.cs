using System.Collections;
using Script;
using Script.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ResourceManager = Script.Manager.ResourceManager;

public class CardSlot : MonoBehaviour
{
    public CharacterData characterData { get; private set; }
    [SerializeField] private Image creatureSprite;
    [SerializeField] private Text cost;
    private Image frameImage;

    private Color initialColor, whiteColor;

    private bool isCooling;
    private float countCoolDown = 0;
    private int coolDown = 5;

    void Awake()
    {
        initialColor = creatureSprite.color;
        whiteColor = Color.white;
    }

    void Start()
    {
        frameImage = GetComponent<Image>();
    }

    void Update()
    {
        if (!isCooling)
        {
            UpdateUI();
        }
    }

    public void InitializeCard(CharacterData data)
    {
        Debug.Log("Add Card" + data.name);
        characterData = data;
        creatureSprite.sprite = data.characterImage;
        cost.text = data.cost.ToString();
    }

    private void UpdateUI()
    {
        if (StageManager.Manager.CurrentCost < characterData.cost)
        {
            float ratio = StageManager.Manager.CurrentCost / characterData.cost;
            creatureSprite.fillAmount = ratio;
        }
        else
        {
            frameImage.color = whiteColor;
            creatureSprite.color = whiteColor;
        }
    }

    public void ResetGauge()
    {
        StartCoroutine(cooldownCoroutine());
    }

    private IEnumerator cooldownCoroutine()
    {
        isCooling = true;

        float elapsed = 0;

        frameImage.color = initialColor;
        creatureSprite.color = initialColor;

        while (elapsed < coolDown)
        {
            elapsed += Time.deltaTime;
            float coolRatio = Mathf.Clamp01(elapsed / coolDown);
            float costRatio = Mathf.Clamp01(StageManager.Manager.CurrentCost / characterData.cost);
            creatureSprite.fillAmount = Mathf.Min(coolRatio, costRatio);
            yield return null;
        }

        isCooling = false;
    }
}