using System;
using System.Collections.Generic;
using Assets.Scripts.Environment;
using Assets.Scripts.General;
using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;
using ParticleEmitter = Assets.Scripts.General.ParticleEmitter;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Enemies
{
    public class EnemyHealthBehavior : MonoBehaviour, IHealthBehavior, IListener<EnemyExplosion>
    {
        public int MaxHealth;
        public Sprite Destroyed;
        public ParticleEmitter PartsEmitter;
        public ParticleEmitter SmokeEmitter;
        public GameObject Explosion;
        public GameObject MiniExplosion;

        public List<Sprite> EnemyAnimation;
        public List<Sprite> EnemyDamagedAnimation;
        public List<Sprite> EnemyDeadAnimation;
        public AnimationController AnimationController;

        public AudioClip EnemyHitSound;
        public AudioClip EnemyDieSound;

        public GameObject Ricochet;
        public GameObject Crater;

        public float ChainReactionDistance = 15f;

        [HideInInspector]
        public int CurrentHealth;

        public float SmokeDelay = .01f;
        private float _currentSmokeDelay;

        private bool _isDead;
        private bool _isDying;
        private float _dyingTimeRemaining = Random.Range(.5f, 1);

        private AudioSource _audioSource;
        private float _pitchRandom = .05f;
        private float _volumeRandom = .15f;

        void Start()
        {
            this.Register<EnemyExplosion>();
            CurrentHealth = MaxHealth;
            _currentSmokeDelay = SmokeDelay;
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        void OnDestroy()
        {
            this.UnRegister<EnemyExplosion>();
        }

        void Update()
        {
            if (CurrentHealth < MaxHealth)
            {
                _currentSmokeDelay -= Time.deltaTime;
                if (_currentSmokeDelay <= 0 && !_isDead)
                {
                    _currentSmokeDelay = SmokeDelay;
                    SmokeEmitter.Fire((float) CurrentHealth < 0 ? 0 : CurrentHealth/(float) MaxHealth);
                }
            }

            if (_isDying)
            {
                UpdateDying();
            }

        }

        private float _miniExplosionDelay;
        private void UpdateDying()
        {
            _miniExplosionDelay -= Time.deltaTime;
            _dyingTimeRemaining -= Time.deltaTime;

            if (_dyingTimeRemaining > 0)
            {
                if (_miniExplosionDelay < 0)
                {
                    _miniExplosionDelay = .1f;

                    var explosion = Instantiate(MiniExplosion);
                    explosion.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-2f, 2f));
                    explosion.transform.rotation = transform.rotation;
                    explosion.transform.SetParent(transform);
                }
            }
            else
            {
                _isDying = false;
                Die(new DamageContext());
            }
        }

        public void TakeDamage(DamageContext context)
        {
            CurrentHealth -= context.Damage;
            PartsEmitter.Fire(Vector3.zero, 20, Random.Range(1, 3));

            CreateRicochet(context);
            PlayClip(EnemyHitSound);

            if (CurrentHealth <= 0)
            {
                StartDying();
                //Die(context);
            }
        }

        private void CreateRicochet(DamageContext context)
        {
            var ricochet = Instantiate(Ricochet);
            ricochet.transform.position = context.Position;
            ricochet.transform.rotation = Quaternion.LookRotation(context.Direction, transform.up);
            ricochet.transform.Rotate(0, 180, 0);
            ricochet.transform.SetParent(transform);
        }

        private void PlayClip(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
            _audioSource.pitch = 1 + Random.Range(-_pitchRandom, _pitchRandom);
            _audioSource.volume = 1 - Random.Range(0, _volumeRandom);
        }

        public void StartDying()
        {
            if (_isDead) return;

            _isDying = true;

            var navMesh = GetComponent<NavMeshAgent>();
            var currentVelocity = navMesh != null ? navMesh.velocity : Vector3.zero;
            var rigidBody = GetComponent<Rigidbody>();
            Destroy(GetComponent<BoxCollider>());
            Destroy(GetComponent<PlayerFollow>());
            Destroy(navMesh);
            GetComponentInChildren<SpriteRenderer>().sprite = Destroyed;

            rigidBody.isKinematic = false;
            rigidBody.velocity = currentVelocity;

            PartsEmitter.Fire(currentVelocity, 20, Random.Range(5, 10));
        }

        public void Die(DamageContext context)
        {
            if (!_isDead)
            {
                PlayClip(EnemyDieSound);
                EventAggregator.SendMessage(new ShakeCamera());
                EventAggregator.SendMessage(new EnemyDied());
                _isDead = true;

                var navMesh = GetComponent<NavMeshAgent>();
                var currentVelocity = navMesh != null ? navMesh.velocity : Vector3.zero;

                Destroy(navMesh);
                Destroy(GetComponent<PlayerFollow>());
                GetComponentInChildren<SpriteRenderer>().sprite = Destroyed;

                Destroy(GetComponent<Rigidbody>());
                Destroy(GetComponent<BoxCollider>());
//                rigidBody.isKinematic = false;
//                rigidBody.velocity = currentVelocity;

                PartsEmitter.Fire(currentVelocity, 20, Random.Range(5, 10));

                var explosion = Instantiate(Explosion);
                explosion.transform.position = transform.position;
                explosion.transform.SetParent(Hack.Everything.transform);

                var crater = Instantiate(Crater);
                crater.transform.position = transform.position;
                crater.transform.rotation = transform.rotation;
                crater.transform.SetParent(Hack.Everything.transform);

                EventAggregator.SendMessage(new EnemyExplosion { Position = transform.position });
            }
        }

        public void Handle(EnemyExplosion message)
        {
            if (Vector3.Distance(transform.position, message.Position) < ChainReactionDistance)
            {
                StartDying();
                //Die(new DamageContext());
            }
        }
    }
}