using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaManager : MonoBehaviour
{
    [System.Serializable]
    public class GachaItem
    {
        public CharacterData characterData;
        public float probability;
    }

    [System.Serializable]
    public class GachaPool
    {
        public string poolName;
        public List<GachaItem> gachaItems;
        [HideInInspector]
        public float totalProbability;
    }

    public List<GachaPool> gachaPools;
    public int currentPoolIndex = 0;

    public GachaEffectController gachaEffectController;
    public GachaCurrencyManager currencyManager;

    public CharacterManager characterManager;

    private void Start()
    {
        foreach (var pool in gachaPools)
        {
            pool.totalProbability = 0f;
            foreach (var item in pool.gachaItems)
            {
                pool.totalProbability += item.probability;
            }
        }
    }

    public CharacterData RollOne()
    {
        if (currentPoolIndex < 0 || currentPoolIndex >= gachaPools.Count)
        {
            Debug.LogWarning("잘못된 가챠 풀 인덱스");
            return null;
        }

        var pool = gachaPools[currentPoolIndex];
        float rand = Random.Range(0f, pool.totalProbability);
        float cumulative = 0f;

        foreach (var item in pool.gachaItems)
        {
            cumulative += item.probability;
            if (rand <= cumulative)
            {
                return item.characterData;
            }
        }

        Debug.LogWarning("확률 계산 오류");
        return null;
    }

    public void OnClickRollOne()
    {
        if (currencyManager != null && currencyManager.HasEnough(1))
        {
            currencyManager.Spend(1);

            var character = RollOne();
            if (character == null) return;

            gachaEffectController?.PlayGachaEffect(character);
            characterManager?.UnlockCharacter(character.characterType);
            UnlockedCharacterManager.Instance?.Unlock(character.characterName);
        }
        else
        {
            Debug.Log("재화가 부족합니다.");
        }
    }

    public void OnClickRollFive()
    {
        if (currencyManager != null && currencyManager.HasEnough(5))
        {
            currencyManager.Spend(5);
            StartCoroutine(RollFiveWithEffect());
        }
        else
        {
            Debug.Log("재화가 부족합니다.");
        }
    }

    private IEnumerator RollFiveWithEffect()
    {
        List<CharacterData> results = new List<CharacterData>();

        for (int i = 0; i < 5; i++)
        {
            var character = RollOne();
            results.Add(character);

            if (gachaEffectController != null && character != null)
            {
                gachaEffectController.PlayGachaEffect(character);
                characterManager?.UnlockCharacter(character.characterType);
                UnlockedCharacterManager.Instance?.Unlock(character.characterName);
            }

            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        gachaEffectController?.ShowMultipleResults(results);
    }

    public void SetGachaPool(int poolIndex)
    {
        if (poolIndex >= 0 && poolIndex < gachaPools.Count)
        {
            currentPoolIndex = poolIndex;
            Debug.Log($"가챠 풀 변경: {gachaPools[poolIndex].poolName}");
        }
        else
        {
            Debug.LogWarning("잘못된 가챠 풀 인덱스");
        }
    }
}
