using Assets.Scripts.Enemies;
using Assets.Scripts.General;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Bullet : MonoBehaviour
    {
        public float Speed;
        public int Damage;

        void Update()
        {
            transform.Translate(transform.forward * Speed);
        }

        void OnTriggerEnter(Collider collider)
        {
            if (collider.tag == "Enemy")
            {
                var context = new DamageContext()
                {
                    Damage = Damage,
                    Force = Speed,
                    Direction = transform.forward
                };

                var health = collider.GetComponent<EnemyHealthBehavior>();
                if (health != null)
                {
                    health.TakeDamage(context);
                }
            }

            if (collider.tag != "Ground" && collider.tag != "Player")
            {
                Destroy(gameObject);
            }
        }
    }
}