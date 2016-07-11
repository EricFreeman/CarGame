using Assets.Scripts.General;
using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;
using ParticleEmitter = Assets.Scripts.General.ParticleEmitter;

namespace Assets.Scripts.Enemies
{
    public class EnemyHealthBehavior : MonoBehaviour, IHealthBehavior
    {
        public int MaxHealth;
        public Sprite Destroyed;
        public ParticleEmitter PartsEmitter;
        public ParticleEmitter SmokeEmitter;
        public GameObject Explosion;

        [HideInInspector]
        public int CurrentHealth;

        public float SmokeDelay = .01f;
        private float _currentSmokeDelay;

        private bool _isDead;

        void Start()
        {
            CurrentHealth = MaxHealth;
            _currentSmokeDelay = SmokeDelay;
        }

        void Update()
        {
            if (CurrentHealth < MaxHealth)
            {
                _currentSmokeDelay -= Time.deltaTime;
                if (_currentSmokeDelay <= 0)
                {
                    _currentSmokeDelay = SmokeDelay;
                    SmokeEmitter.Fire((float) CurrentHealth < 0 ? 0 : CurrentHealth/(float) MaxHealth);
                }
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
                EventAggregator.SendMessage(new ShakeCamera());
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

                var explosion = Instantiate(Explosion);
                explosion.transform.position = transform.position;
            }
        }
    }
}