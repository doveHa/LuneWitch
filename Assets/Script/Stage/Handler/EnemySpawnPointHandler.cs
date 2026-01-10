using UnityEngine;

namespace Script.Stage.Handler
{
    public class EnemySpawnPointHandler : MonoBehaviour
    {
        private ParticleSystem warningStep;

        void Start()
        {
            warningStep = GetComponentInChildren<ParticleSystem>();
        }

        public void ShowWarningStep()
        {
            warningStep.Play();
        }

        public bool IsPlayingParticle()
        {
            return warningStep.isPlaying;
        }
    }
}