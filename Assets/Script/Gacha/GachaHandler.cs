using System;
using System.Collections;
using System.Collections.Generic;
using Script.DataDefinitions.ScriptableObjects;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Manager
{
    public class GachaHandler : MonoBehaviour
    {
        [Serializable]
        public class GachaItem
        {
            public CharacterData characterData;
            public float probability;
        }

        [Serializable]
        public class GachaPool
        {
            public string poolName;
            public List<GachaItem> gachaItems;
            [HideInInspector] public float totalProbability;
        }

        public List<GachaPool> gachaPools;
        public int currentPoolIndex = 0;

        public GachaEffectHandler gachaEffectHandler;

        public CharacterManager characterManager;

        [SerializeField] private TextMeshProUGUI currentMoonStone; 
        private int MoonStone = 100;

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

            UpdateMoonStone();
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
            StartCoroutine(Gacha(1));
        }

        public void OnClickRollFive()
        {
            StartCoroutine(Gacha(5));
        }

        private IEnumerator Gacha(int count)
        {
            if (MoonStone < count)
            {
                Debug.Log("재화가 부족합니다.");
                yield break;
            }

            MoonStone -= count;

            List<CharacterData> results = new List<CharacterData>();

            for (int i = 0; i < count; i++)
            {
                var character = RollOne();
                if (character == null) continue;

                results.Add(character);

                // 연출 및 해금 처리
                if (gachaEffectHandler != null)
                {
                    gachaEffectHandler.PlayGachaEffect(character);
                }

                characterManager?.UnlockCharacter(character.characterType);
                UnlockedCharacterManager.Manager.Unlock(character.characterName);

                if (count > 1)
                {
                    yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
                    yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                }
            }

            if (count > 1)
            {
                gachaEffectHandler?.ShowMultipleResults(results);
            }
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

        private void UpdateMoonStone()
        {
            currentMoonStone.text = MoonStone.ToString();
        }
    }
}