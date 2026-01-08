using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Boss.Handler
{
    public class BossAttackHandler : MonoBehaviour
    {
        public Action ActiveBossPattern(int patternIndex)
        {
            switch (patternIndex)
            {
                case 1:
                    return Pattern1;
                case 2:
                    return Pattern2;
                case 3:
                    return Pattern3;
            }

            return null;
        }

        public int GetAttackPatternIndex()
        {
            return Random.Range(1, 4);
        }

        private void Pattern1()
        {
            Debug.Log("Pattern 1");
        }

        private void Pattern2()
        {
            Debug.Log("Pattern 2");

        }

        private void Pattern3()
        {
            Debug.Log("Pattern 3");
        }
    }
}