/*
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.Json;
using TMPro;
using UnityEngine.UI;
using Script.Manager;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class Item
    {
        public Item(string type, string name, string hp, string attack, string attack1, string mana,
            string introduction)
        {
            Type = type;
            Name = name;
            this.hp = hp;
            Attack = attack;
            Attack1 = attack1;
            this.mana = mana;
            this.Introduction = introduction;
            isUnlocked = false;
        }

        public string Type { get; set; }
        public string Name { get; set; }
        public string hp { get; set; }
        public string Attack { get; set; }
        public string Attack1 { get; set; }
        public string mana { get; set; }
        public string Introduction { get; set; }
        public bool isUnlocked;
    }

    public TextAsset list;
    public List<Item> AllItemLIst = new List<Item>(), MyItemLIst = new List<Item>(), CurItemLIst = new List<Item>();
    public string curType = "All";

    public GameObject[] Slot;
    public Image[] TabImage, ItemImage;
    public Sprite[] TabIdleSprites;
    public Sprite[] TabActiveSprites;
    public Sprite[] ItemSprite;

    public GameObject InfoPanel;
    public TextMeshProUGUI InfoNameText;
    public TextMeshProUGUI InfoHpText;
    public TextMeshProUGUI InfoAttackText;
    public TextMeshProUGUI InfoAttack1Text;
    public TextMeshProUGUI InfoManaText;
    public TextMeshProUGUI InfoDescText;
    public Image InfoItemImage;

    public GameObject CollectionPanel;
    public Animator descriptionAnimator;

    public TextMeshProUGUI itemCountText; // 👈 획득 수 텍스트 변수 추가

    private Item currentSelectedItem = null;
    private string savePath;

    void Start()
    {
        savePath = Application.persistentDataPath + "/MyItemText.txt";

        string[] line = list.text.Substring(0, list.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split(',');
            AllItemLIst.Add(new Item(row[0], row[1], row[2], row[3], row[4], row[5], row[6]));
        }

        Load();
    }

    public void TabClick(string tabName)
    {
        curType = tabName;

        if (tabName == "All")
            CurItemLIst = new List<Item>(MyItemLIst);
        else
            CurItemLIst = MyItemLIst.FindAll(x => x.Type == tabName);

        CurItemLIst.Sort((a, b) => b.isUnlocked.CompareTo(a.isUnlocked));

        for (int i = 0; i < Slot.Length; i++)
        {
            bool isExist = i < CurItemLIst.Count;
            Slot[i].SetActive(isExist);

            if (isExist)
            {
                Item item = CurItemLIst[i];

                Transform lockObj = Slot[i].transform.Find("LockObject");
                if (lockObj != null)
                    lockObj.gameObject.SetActive(!item.isUnlocked);

                Slot[i].GetComponentInChildren<TextMeshProUGUI>().text = item.isUnlocked ? item.Name : "???";

                int index = AllItemLIst.FindIndex(x => x.Name == item.Name);
                if (index >= 0 && index < ItemSprite.Length)
                {
                    ItemImage[i].sprite = ItemSprite[index];
                    ItemImage[i].preserveAspect = true;
                }

                int capturedIndex = i;
                Button btn = Slot[i].GetComponent<Button>();
                if (btn != null)
                {
                    btn.onClick.RemoveAllListeners();
                    if (item.isUnlocked)
                        btn.onClick.AddListener(() => OnSlotClick(capturedIndex));
                    else
                        btn.onClick.AddListener(() => Debug.Log("해당 아이템은 아직 해금되지 않았습니다."));
                }
            }
        }

        int tabNum = tabName switch
        {
            "All" => 0,
            "Defense" => 1,
            "Attack" => 2,
            "Attack2" => 3,
            _ => 0
        };

        for (int i = 0; i < TabImage.Length; i++)
            TabImage[i].sprite = (i == tabNum) ? TabActiveSprites[i] : TabIdleSprites[i];

        UpdateItemCountText(); // 👈 탭 전환 시 획득 수 업데이트
    }

    public void OnSlotClick(int index)
    {
        if (index < CurItemLIst.Count)
        {
            Item selectedItem = CurItemLIst[index];
            if (!selectedItem.isUnlocked) return;
            if (currentSelectedItem != null && selectedItem.Name == currentSelectedItem.Name) return;

            currentSelectedItem = selectedItem;
            InfoPanel.SetActive(true);

            InfoNameText.text = selectedItem.Name;
            InfoHpText.text = selectedItem.hp;
            InfoAttackText.text = selectedItem.Attack;
            InfoAttack1Text.text = selectedItem.Attack1;
            InfoManaText.text = selectedItem.mana;
            InfoDescText.text = selectedItem.Introduction;

            int spriteIndex = AllItemLIst.FindIndex(x => x.Name == selectedItem.Name);
            if (spriteIndex >= 0 && spriteIndex < ItemSprite.Length)
            {
                InfoItemImage.sprite = ItemSprite[spriteIndex];
                InfoItemImage.preserveAspect = true;
            }

            if (descriptionAnimator != null)
                descriptionAnimator.SetTrigger("doshow");
        }
    }

    public void CloseInfoPanel()
    {
        InfoPanel.SetActive(false);
        currentSelectedItem = null;
    }

    void Save()
    {
        string jdata = JsonSerializer.Serialize(MyItemLIst);
        File.WriteAllText(savePath, jdata);
    }

    void Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);

            MyItemLIst = JsonSerializer.Deserialize<List<Item>>(json);
        }
        else
        {
            MyItemLIst = new List<Item>();
            foreach (var item in AllItemLIst)
            {
                Item newItem = new Item(item.Type, item.Name, item.hp, item.Attack, item.Attack1, item.mana,
                    item.Introduction);
                if (item.Name == "실룸" || item.Name == "펌피" || item.Name == "마력석" || item.Name == "리멜른")
                    newItem.isUnlocked = true;
                MyItemLIst.Add(newItem);
            }

            Save();
        }

        TabClick(curType);
    }

    public void OpenCollection()
    {
        CollectionPanel.SetActive(true);
        ResetUI();
        UpdateItemCountText(); // 👈 컬렉션 열 때 업데이트
    }

    public void CloseCollection()
    {
        CollectionPanel.SetActive(false);
    }

    public void ResetUI()
    {
        curType = "All";
        TabClick(curType);
        CloseInfoPanel();
    }

    public void UnlockItem(string itemName)
    {
        Item found = MyItemLIst.Find(x => x.Name == itemName);
        if (found != null)
        {
            found.isUnlocked = true;
        }
        else
        {
            Item fromAll = AllItemLIst.Find(x => x.Name == itemName);
            if (fromAll != null)
            {
                Item newItem = new Item(fromAll.Type, fromAll.Name, fromAll.hp, fromAll.Attack, fromAll.Attack1,
                    fromAll.mana, fromAll.Introduction);
                newItem.isUnlocked = true;
                MyItemLIst.Add(newItem);
            }
        }

        // ✅ 덱 선택용 해금 매니저에도 추가
        if (UnlockedCharacterManager.Manager != null)
        {
            UnlockedCharacterManager.Manager.Unlock(itemName);
        }

        Save();
        TabClick(curType);
        UpdateItemCountText();
    }

    public void ResetSaveData()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("저장 데이터 초기화됨.");
        }

        MyItemLIst = new List<Item>();
        foreach (var item in AllItemLIst)
        {
            Item newItem = new Item(item.Type, item.Name, item.hp, item.Attack, item.Attack1, item.mana,
                item.Introduction);
            if (item.Name == "실룸" || item.Name == "펌피" || item.Name == "마력석" || item.Name == "리멜른")
                newItem.isUnlocked = true;
            MyItemLIst.Add(newItem);
        }

        Save();
        ResetUI();
        UpdateItemCountText(); // 👈 초기화 후 업데이트
    }

    // 👇 획득 수 업데이트 함수
    private void UpdateItemCountText()
    {
        int unlockedCount = MyItemLIst.FindAll(x => x.isUnlocked).Count;
        int totalCount = AllItemLIst.Count;
        itemCountText.text = $"{unlockedCount}/{totalCount}";
    }
}*/