using Assets.Scripts.General;
using UnityEngine;
using ParticleEmitter = Assets.Scripts.General.ParticleEmitter;

namespace Assets.Scripts.Enemies
{
    public class EnemyHealthBehavior : MonoBehaviour, IHealthBehavior
    {
        public int MaxHealth;
        public Sprite Destroyed;
        public ParticleEmitter PartsEmitter;
        public ParticleEmitter SmokeEmitter;

        [HideInInspector]
        public int CurrentHealth;

        private bool _isDead;

        void Start()
        {
            CurrentHealth = MaxHealth;
        }

        void Update()
        {
            if (CurrentHealth < MaxHealth)
            {
                SmokeEmitter.Fire((float)CurrentHealth < 0 ? 0 : CurrentHealth / (float)MaxHealth);
            }
        }

        public void TakeDamage(DamageContext context)
        {
            CurrentHealth -= context.Damage;
            PartsEmitter.Fire(Vector3.zero, 20, Random.Range(1, 3));

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

                PartsEmitter.Fire(currentVelocity, 20, Random.Range(5, 10));
            }
        }
    }
}