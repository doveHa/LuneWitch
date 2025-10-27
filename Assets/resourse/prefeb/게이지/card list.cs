using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.Json;
using TMPro;

public class cardlist : MonoBehaviour
{
    [System.Serializable]
    public class Item
    {
        public Item(string Name, string Attack, string hp)
        { this.Name = Name; this.Attack = Attack; this.hp = hp; }

        public string Name { get; set; }
        public string Attack { get; set; }
        public string hp { get; set; }
    }

    public TextAsset ItemDatabase;
    public List<Item> AllItemList = new List<Item>();
    public List<Item> MyItemList = new List<Item>();

    public GameObject ExplainPanel;
    public RectTransform CanvasRect;

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI AttackText;
    public TextMeshProUGUI HpText;

    IEnumerator PointerCoroutine;
    public Vector2 v;

    void Start()
    {
        // CSV �������� ItemDatabase �о AllItemList�� ����
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split(',');
            AllItemList.Add(new Item(row[0], row[1], row[2]));
        }

        Load();
    }

    void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvasRect, Input.mousePosition, Camera.main, out Vector2 anchoredPos);
        ExplainPanel.GetComponent<RectTransform>().anchoredPosition = anchoredPos + v;
    }

    public void PointerEnter(int sloNum)
    {
        PointerCoroutine = PointerEnterDelay(sloNum);
        StartCoroutine(PointerCoroutine);
    }

    IEnumerator PointerEnterDelay(int sloNum)
    {
        yield return new WaitForSeconds(0.5f);

        if (sloNum >= 0 && sloNum < MyItemList.Count)
        {
            Item item = MyItemList[sloNum];

            NameText.text = $"{item.Name}";
            AttackText.text = $"{item.Attack}";
            HpText.text = $"{item.hp}";
        }

        ExplainPanel.SetActive(true);
    }

    public void PointerExit(int sloNum)
    {
        if (PointerCoroutine != null)
        {
            StopCoroutine(PointerCoroutine);
        }

        ExplainPanel.SetActive(false);

        NameText.text = "";
        AttackText.text = "";
        HpText.text = "";
    }

    void Save()
    {
        string jdata = JsonSerializer.Serialize(AllItemList);
        string path = Application.persistentDataPath + "/cardlistText.json";
        File.WriteAllText(path, jdata);
        Debug.Log("Saved to " + path);
    }

    void Load()
    {
        string path = Application.persistentDataPath + "/cardlistText.json";

        if (File.Exists(path))
        {
            string jdata = File.ReadAllText(path);
            MyItemList = JsonSerializer.Deserialize<List<Item>>(jdata);
            Debug.Log("Loaded from " + path);
        }
        else
        {
            Debug.LogWarning("File not found at " + path + ", initializing with default data.");
            MyItemList = new List<Item>(AllItemList);
            Save();
        }
    }
}
