using TMPro;
using UnityEngine;

public class GachaCurrencyManager : MonoBehaviour
{
    public TMP_Text currencyText;

    private void Start()
    {
        UpdateUI();
    }

    public bool HasEnough(int amount)
    {
        return PlayerData.GachaTokens >= amount;
    }

    public void Spend(int amount)
    {
        PlayerData.GachaTokens -= amount;
        UpdateUI();
    }

    public void Add(int amount)
    {
        PlayerData.GachaTokens += amount;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (currencyText != null)
        {
            currencyText.text = PlayerData.GachaTokens.ToString();
        }
    }
}
