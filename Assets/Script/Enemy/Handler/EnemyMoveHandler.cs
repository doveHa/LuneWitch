using System.Collections;
using UnityEngine;

namespace Script.Enemy.Handler
{
    public class EnemyMoveHandler : MonoBehaviour
    {
        public float Speed { get; set; } = 0.5f;
        private float knockBackTime = 0.5f;
        private Rigidbody2D rigidbody;

        void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Initialize(float speed)
        {
            Speed = speed;
        }

        public void StartWalk()
        {
            rigidbody.linearVelocity = Vector2.left * Speed;
        }

        public void StopWalk()
        {
            rigidbody.linearVelocity = Vector2.zero;
        }

        public void KnockBack(float knockBack)
        {
            StartCoroutine(Slide(knockBack));
        }

        private IEnumerator Slide(float knockBack)
        {
            rigidbody.linearVelocity = Vector2.zero;

            Vector2 startPos = transform.position;
            Vector2 endPos = new Vector2(startPos.x + knockBack, startPos.y);
            float elapsedTime = 0f;

            while (elapsedTime < knockBackTime)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / knockBackTime;
                float easeValue = 1f - Mathf.Pow(1f - t, 3);

                transform.position = Vector3.Lerp(startPos, endPos, easeValue);

                yield return null;
            }

            transform.position = endPos;
        }
    }
}