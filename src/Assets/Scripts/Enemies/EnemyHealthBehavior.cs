using Assets.Scripts.General;
using System;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class EnemyHealthBehavior : MonoBehaviour, IHealthBehavior
    {
        public int MaxHealth;
        public Sprite Destroyed;

        [HideInInspector]
        public int CurrentHealth;

        private bool _isDead;

        void Start()
        {
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(DamageContext context)
        {
            CurrentHealth -= context.Damage;

            if (CurrentHealth <= 0)
            {
                Die(context);
            }
        }

        public void Die(DamageContext context)
        {
            if (!_isDead)
            {
                _isDead = true;

                var navMesh = GetComponent<NavMeshAgent>();
                var currentVelocity = navMesh.velocity;

                Destroy(navMesh);
                Destroy(GetComponent<PlayerFollow>());
                GetComponentInChildren<SpriteRenderer>().sprite = Destroyed;

                var rigidBody = GetComponent<Rigidbody>();
                rigidBody.isKinematic = false;
                rigidBody.velocity = currentVelocity;
//            rigidBody.AddForce(context.Direction * context.Force * 300);
            }
        }
    }
}