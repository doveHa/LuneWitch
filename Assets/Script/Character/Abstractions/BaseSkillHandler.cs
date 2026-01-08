using UnityEngine;

namespace Script.Player
{
    public abstract class BaseSkillHandler : MonoBehaviour
    {
        [SerializeField] protected int damage;
        [SerializeField] protected ParticleSystem particle;
        
        public abstract void OnSkillUse();
    }
}