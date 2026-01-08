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
            
        }

        private void Pattern2()
        {
            
        }

        private void Pattern3()
        {
            
        }
    }
}