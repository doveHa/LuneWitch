using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.Json;
using Script.DataDefinitions.Enum;
using Script.Manager;
using TMPro;
using UnityEngine.UI;


public class CharacterManager : MonoBehaviour
{
    [System.Serializable]
    public class Item
    {
        public Item(string type, string name, string introduction, string skill)
        {
            Type = type;
            Name = name;
            Introduction = introduction;
            Skill = skill;
        }

        public string Type { get; set; }
        public string Name { get; set; }
        public string Introduction { get; set; }
        public string Skill { get; set; }
    }

    public class SaveData
    {
        public bool[] characterActive { get; set; }
        public string selectedCharacter { get; set; }
    }

    public TextAsset Characterlist;
    public List<Item> AllItemLIst = new List<Item>(), MyItemLIst = new List<Item>(), CurItemLIst = new List<Item>();
    public string curType = "루미나";

    public GameObject[] InfoPanel;
    public Image[] TabImage, ItemImage, ItemImage2;
    public Sprite TabIdieSprite, TabSelestSprite;
    public Sprite[] ItemSprite, ItemSprite2;

    public GameObject CharacterWindow;
    public Button[] TabButtons;

    public bool[] CharacterActive = { true, false };

    [Header("선택 캐릭터 설정")] public Image MainPlayerImage;
    public Sprite[] PlayerSprites;

    [Header("텍스트 UI")] public TextMeshProUGUI CharacterNameText;
    public TextMeshProUGUI CharacterDescriptionText;

    [Header("캐릭터 이름")] public string[] CharacterNames = new string[2] { "루미나", "아이린" };

    [Header("캐릭터 설명")] public string[] CharacterDescriptions = new string[2]
    {
        "빛의 마법을 사용하는 밝고 명랑한 소녀.",
        "차분하고 냉정한 얼음 속성의 마법사."
    };

    void Start()
    {
        string[] line = Characterlist.text.Trim().Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split(',');
            AllItemLIst.Add(new Item(row[0], row[1], row[2], row[3]));
        }

        Load();
        UpdateTabButtons();
        SelectCharacter(curType);
    }

    public void OpenCharacterWindow()
    {
        CharacterWindow.SetActive(true);
        ResetUI();
    }

    public void CloseCharacterWindow()
    {
        CharacterWindow.SetActive(false);
    }

    public void ResetUI()
    {
        curType = "루미나";
        TabClick(curType);
    }

    public void TabClick(string tabName)
    {
        int tabIndex = GetCharacterIndex(tabName);
        if (tabIndex == -1 || !CharacterActive[tabIndex])
        {
            Debug.LogWarning($"[{tabName}] 캐릭터가 활성화 되어있지 않아 탭 전환 불가.");
            return;
        }

        curType = tabName;
        CurItemLIst = MyItemLIst.FindAll(x => x.Type == tabName);

        for (int i = 0; i < InfoPanel.Length; i++)
        {
            bool isExist = i < CurItemLIst.Count;
            InfoPanel[i].SetActive(isExist);
            Transform panel = InfoPanel[i].transform;

            panel.Find("NameText").GetComponent<TextMeshProUGUI>().text = isExist ? CurItemLIst[i].Name : "";
            panel.Find("IntroductionText").GetComponent<TextMeshProUGUI>().text =
                isExist ? CurItemLIst[i].Introduction : "";
            panel.Find("SkillText").GetComponent<TextMeshProUGUI>().text = isExist ? CurItemLIst[i].Skill : "";

            if (isExist)
            {
                int index = AllItemLIst.FindIndex(x => x.Name == CurItemLIst[i].Name);

                if (index >= 0 && index < ItemSprite.Length)
                    ItemImage[i].sprite = ItemSprite[index];

                if (index >= 0 && index < ItemSprite2.Length)
                    ItemImage2[i].sprite = ItemSprite2[index];
            }
        }

        for (int i = 0; i < TabImage.Length; i++)
        {
            TabImage[i].sprite = (i == tabIndex) ? TabSelestSprite : TabIdieSprite;
        }
    }

    void UpdateTabButtons()
    {
        for (int i = 0; i < TabButtons.Length; i++)
        {
            //Debug.Log(TabButtons.Length + " " + " " + CharacterActive.Length + " " + CharacterActive[i]);
            bool active = CharacterActive[i];
            TabButtons[i].interactable = active;

            TabImage[i].sprite = active ? TabIdieSprite : TabSelestSprite;
            TabImage[i].color = active ? Color.white : new Color(0.75f, 0.75f, 0.75f);

            Transform faceImageTransform = TabButtons[i].transform.Find("Image");
            if (faceImageTransform != null)
            {
                Image faceImage = faceImageTransform.GetComponent<Image>();
                if (faceImage != null)
                {
                    faceImage.color = active ? Color.white : new Color(0.4f, 0.4f, 0.4f, 1f);
                }
            }
        }
    }

    public void UnlockCharacter(string characterType)
    {
        int index = GetCharacterIndex(characterType);

        if (index != -1 && !CharacterActive[index])
        {
            CharacterActive[index] = true;
            UpdateTabButtons();
            Save();
            Debug.Log($"{characterType} 캐릭터가 활성화 되었습니다.");
        }
    }

    public void SelectCharacter(string characterType)
    {
        int index = GetCharacterIndex(characterType);

        if (index != -1 && CharacterActive[index])
        {
            if (MainPlayerImage != null && index < PlayerSprites.Length)
            {
                MainPlayerImage.sprite = PlayerSprites[index];
                if (CharacterDescriptionText != null) CharacterDescriptionText.text = CharacterDescriptions[index];
                if (CharacterNameText != null) CharacterNameText.text = CharacterNames[index];

                CharacterName name = CharacterName.Lumina;
                switch (characterType)
                {
                    case "루미나":
                        name = CharacterName.Lumina;
                        break;
                    case "아이렌":
                        name = CharacterName.Irene;
                        break;
                }

                PlayerManager.Manager.SelectedCharacter = name;


                Debug.Log($"[{characterType}] 캐릭터가 선택되어 메인 화면에 반영되었습니다.");
            }
        }
    }

    int GetCharacterIndex(string characterType)
    {
        switch (characterType)
        {
            case "루미나": return 0;
            case "아이린": return 1;
            default: return -1;
        }
    }

    void Save()
    {
        //string jdata = JsonConvert.SerializeObject(AllItemLIst);
        string path1 = Path.Combine(Application.persistentDataPath, "MyCharacterItemList.json");
        string itemData = JsonSerializer.Serialize(AllItemLIst);
        //string itemData = JsonConvert.SerializeObject(AllItemLIst, Formatting.Indented);
        File.WriteAllText(path1, itemData);

        SaveData saveData = new SaveData
        {
            characterActive = CharacterActive,
            selectedCharacter = curType
        };

        string path2 = Path.Combine(Application.persistentDataPath, "CharacterSaveData.json");
        string saveJson = JsonSerializer.Serialize(saveData);
        //JsonConvert.SerializeObject(saveData, Formatting.Indented);
        File.WriteAllText(path2, saveJson);
    }


    void Load()
    {
        string path1 = Path.Combine(Application.persistentDataPath, "MyCharacterItemList.json");
        if (File.Exists(path1))
        {
            string jdata = File.ReadAllText(path1);
            MyItemLIst = JsonSerializer.Deserialize<List<Item>>(jdata);
//                JsonConvert.DeserializeObject<List<Item>>(jdata);
        }
        else
        {
            TextAsset jsonFile = Resources.Load<TextAsset>("MyCharacterItemList");
            if (jsonFile != null)
            {
                //MyItemLIst = JsonConvert.DeserializeObject<List<Item>>(jsonFile.text);
                MyItemLIst = JsonSerializer.Deserialize<List<Item>>(jsonFile.text);

                File.WriteAllText(path1, jsonFile.text);
            }
            else
            {
                Debug.LogWarning("기본 캐릭터 데이터 파일이 Resources/MyCharacterItemList.txt에 없습니다.");
                MyItemLIst = new List<Item>();
            }
        }

        string path2 = Path.Combine(Application.persistentDataPath, "CharacterSaveData.json");
        if (File.Exists(path2))
        {
            string saveJson = File.ReadAllText(path2);
            Debug.Log(saveJson);
            SaveData saveData = JsonSerializer.Deserialize<SaveData>(saveJson);

            CharacterActive = saveData.characterActive;
            curType = saveData.selectedCharacter;
        }
    }

    void OnApplicationQuit()
    {
        Save();
    }

    // ───────────── 추가 부분 ─────────────

    // 저장 데이터 초기화 함수
    public void ResetSaveData()
    {
        string path1 = Path.Combine(Application.persistentDataPath, "MyCharacterItemList.json");
        string path2 = Path.Combine(Application.persistentDataPath, "CharacterSaveData.json");

        if (File.Exists(path1))
            File.Delete(path1);

        if (File.Exists(path2))
            File.Delete(path2);

        // 초기 상태 (루미나만 활성화)
        CharacterActive = new bool[2] { true, false };
        curType = "루미나";

        // 아이템 리스트도 초기화 (AllItemLIst 기준)
        MyItemLIst = new List<Item>();
        foreach (var item in AllItemLIst)
        {
            if (item.Type == curType)
                MyItemLIst.Add(item);
        }

        UpdateTabButtons();
        ResetUI();
        Save();

        Debug.Log("저장 데이터가 초기화 되었습니다.");
    }

    // UI 버튼에 연결할 함수
    public void OnResetSaveButtonClicked()
    {
        ResetSaveData();
    }
}