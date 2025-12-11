using UnityEngine;

namespace Script.Enemy
{
    public class EnemyMoveHandler : MonoBehaviour
    {
        public float Speed { get; set; } = 0.5f;
        private Rigidbody2D rigidbody;

        void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            Walk();
        }

        public void Initialize(float speed)
        {
            Speed = speed;
        }

        public void Walk()
        {
            rigidbody.linearVelocity = Vector2.left * Speed;
        }

        public void StopWalk()
        {
            rigidbody.linearVelocity = Vector2.zero;
        }
    }
}