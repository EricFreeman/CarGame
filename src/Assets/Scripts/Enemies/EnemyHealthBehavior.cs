using System.Collections.Generic;
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

        public List<Sprite> EnemyAnimation;
        public List<Sprite> EnemyDamagedAnimation;
        public List<Sprite> EnemyDeadAnimation;
        public AnimationController AnimationController;

        public AudioClip EnemyHitSound;
        public AudioClip EnemyDieSound;

        public GameObject Ricochet;

        [HideInInspector]
        public int CurrentHealth;

        public float SmokeDelay = .01f;
        private float _currentSmokeDelay;

        private bool _isDead;

        private AudioSource _audioSource;
        private float _pitchRandom = .05f;
        private float _volumeRandom = .15f;

        void Start()
        {
            CurrentHealth = MaxHealth;
            _currentSmokeDelay = SmokeDelay;
//            AnimationController.PlayAnimation(EnemyAnimation, AnimationType.Loop);
            _audioSource = gameObject.AddComponent<AudioSource>();
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
        }

        public void TakeDamage(DamageContext context)
        {
            CurrentHealth -= context.Damage;
            PartsEmitter.Fire(Vector3.zero, 20, Random.Range(1, 3));

            CreateRicochet(context);
            PlayClip(EnemyHitSound);

            if (CurrentHealth <= 0)
            {
                Die(context);
            }
            else
            {
//                AnimationController.PlayAnimation(EnemyDamagedAnimation, AnimationType.OneOff);
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

        public void Die(DamageContext context)
        {
            if (!_isDead)
            {
                PlayClip(EnemyDieSound);
//                AnimationController.PlayAnimation(EnemyDeadAnimation, AnimationType.Loop);
                EventAggregator.SendMessage(new ShakeCamera());
                EventAggregator.SendMessage(new EnemyDied());
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