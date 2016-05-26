using System.Collections.Generic;
using Assets.Scripts.Extensions;
using UnityEngine;

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
                    obj.GetComponent<Rigidbody>().AddForce(
                        new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * ParticleForce, 
                        ForceMode.Impulse);
                }
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

                var rigidBody = obj.GetComponent<Rigidbody>();
                rigidBody.velocity = initialVelocity;
                obj.GetComponent<Rigidbody>().AddForce(
                    new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f))*particleForce,
                    ForceMode.Impulse);
            }
        }
    }
}
