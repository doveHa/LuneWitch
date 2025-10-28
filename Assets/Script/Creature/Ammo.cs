using UnityEngine;

namespace Script.Creature
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ammo : MonoBehaviour
    {
        private int atk;
        private float ammoSpeed;

        public void SetStat(int atk, float ammoSpeed)
        {
            this.atk = atk;
            this.ammoSpeed = ammoSpeed;
        }

        public void AddForce()
        {
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.right * ammoSpeed;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
            }
        }
    }
}