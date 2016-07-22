using System.Collections.Generic;
using Assets.Scripts.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.General
{
    public class ParticleEmitter : MonoBehaviour
    {
        public bool IsOneShot = true;
        public bool IsSelfFire = false;
        public int SpawnsPerSecond = 5;
        public float ParticleForce = 25f;

        private bool _isEmitting;

        public GameObject ParticleGameObject; 
        public List<Sprite> SpriteList;

        void Start()
        {
            if (IsSelfFire)
            {
                Fire();
            }
        }

        void Update()
        {
            if (_isEmitting)
            {
                if (IsOneShot)
                {
                    _isEmitting = false;
                }

                for (var i = 0; i < SpawnsPerSecond; i++)
                {
                    var obj = Instantiate(ParticleGameObject);
                    obj.GetComponentInChildren<SpriteRenderer>().sprite = SpriteList.Random();
                    obj.transform.position = transform.position + new Vector3(0, .1f, 0);

                    var rigidBody = obj.GetComponent<Rigidbody>();
                    if (rigidBody != null)
                    {
                        rigidBody.AddForce(
                            new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f))*
                            ParticleForce,
                            ForceMode.Impulse);
                    }
                }
            }
        }

        public void Fire(float colorMultiplier)
        {
            var obj = Instantiate(ParticleGameObject);
            var spriteRenderer = obj.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.sprite = SpriteList.Random();

            var color = spriteRenderer.color;
            spriteRenderer.color = new Color(color.r * colorMultiplier, color.g * colorMultiplier, color.b * colorMultiplier, 1);

            obj.transform.position = transform.position + new Vector3(0, .1f, 0);

            var rigidBody = obj.GetComponent<Rigidbody>();
            if (rigidBody != null)
            {
                rigidBody.AddForce(
                    new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f))*ParticleForce,
                    ForceMode.Impulse);
            }
        }

        public void Fire()
        {
            _isEmitting = true;
        }

        public void Fire(Vector3 initialVelocity, float particleForce, float particleCount)
        {
            for (var i = 0; i < particleCount; i++)
            {
                var obj = Instantiate(ParticleGameObject);
                obj.GetComponentInChildren<SpriteRenderer>().sprite = SpriteList.Random();
                obj.transform.position = transform.position + new Vector3(0, .1f, 0);

                var particle = obj.GetComponent<Particle>();
                particle.Velocity = initialVelocity + (
                    new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f))*particleForce);
            }
        }
    }
}
