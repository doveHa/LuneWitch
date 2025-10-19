using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.Json;

public class UnlockedCharacterManager : MonoBehaviour
{
    private HashSet<string> unlockedCharacterNames = new HashSet<string>();
    public List<CharacterData> allCharacters;

    public static UnlockedCharacterManager Instance;

    private string savePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            savePath = Application.persistentDataPath + "/UnlockedCharacters.json";
            LoadUnlockedCharacters();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // �⺻ �ر� ĳ���� (�ߺ� �ر� ����)
        UnlockIfNotAlready("���¼�");
        UnlockIfNotAlready("����");
        UnlockIfNotAlready("�Ƿ�");
        UnlockIfNotAlready("���ḥ");
    }

    // �ߺ� �ر� ������ �Լ�
    private void UnlockIfNotAlready(string characterName)
    {
        if (!IsUnlocked(characterName))
        {
            Unlock(characterName);
        }
    }

    public void Unlock(string characterName)
    {
        if (!unlockedCharacterNames.Contains(characterName))
        {
            unlockedCharacterNames.Add(characterName);
            Debug.Log($"[�ر�] {characterName}");
            SaveUnlockedCharacters(); // �ر� �� ����
        }
    }

    public bool IsUnlocked(string characterName)
    {
        return unlockedCharacterNames.Contains(characterName);
    }

    public List<CharacterData> GetUnlockedCharacters()
    {
        List<CharacterData> result = new List<CharacterData>();
        foreach (var c in allCharacters)
        {
            if (IsUnlocked(c.characterName))
                result.Add(c);
        }
        return result;
    }

    public void SaveUnlockedCharacters()
    {
        List<string> saveList = new List<string>(unlockedCharacterNames);
        string json = JsonSerializer.Serialize(saveList);
        File.WriteAllText(savePath, json);
    }

    public void LoadUnlockedCharacters()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            List<string> loadList = JsonSerializer.Deserialize<List<string>>(json);
            unlockedCharacterNames = new HashSet<string>(loadList);
        }
    }

    public void ResetUnlockedCharacters()
    {
        unlockedCharacterNames.Clear();
        SaveUnlockedCharacters();
    }
}
