using TMPro;
using UnityEngine;

public class MainUI : MonoBehaviour
{
   
    public TextMeshProUGUI gachaText;

    void Start()
    {
        UpdateCurrencyUI();
    }

    public void UpdateCurrencyUI()
    {
       
        gachaText.text = PlayerData.GachaTokens.ToString();
    }
}
