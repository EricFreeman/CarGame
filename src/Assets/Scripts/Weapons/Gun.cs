﻿using Assets.Scripts.Environment;
using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Weapons
{
    public class Gun : MonoBehaviour
    {
        public GameObject BulletGameObject;
        public GameObject Tip;
        public GameObject MuzzleFlare;
        public AudioClip BulletSound;
        public float ShotCooldown;
        public float Accuracy;
        public float Heat;

        private float _shotCooldown;
        private AudioSource _audioSource;
        private float _pitchRandom = .05f;
        private float _volumeRandom = .15f;

        void Start()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        void Update()
        {
            _shotCooldown -= Time.deltaTime;
        }

        public void Shoot()
        {
            if (CanShoot())
            {
                _shotCooldown = ShotCooldown;
                var bullet = Instantiate(BulletGameObject);
                bullet.transform.position = Tip.transform.position;
                bullet.transform.rotation = Tip.transform.rotation;
                bullet.transform.Rotate(0, Random.Range(-Accuracy, Accuracy), 0);
                bullet.transform.SetParent(Hack.Everything.transform);
                _audioSource.PlayOneShot(BulletSound);
                _audioSource.pitch = 1 + Random.Range(-_pitchRandom, _pitchRandom);
                _audioSource.volume = 1 - Random.Range(0, _volumeRandom);

                SpawnMuzzleFlare();

                EventAggregator.SendMessage(new KickBackCamera { KickAmount = .5f, Direction = transform.forward * -1 });
            }
        }

        private void SpawnMuzzleFlare()
        {
            if (MuzzleFlare == null)
            {
                return;
            }

            var muzzleFlare = Instantiate(MuzzleFlare);
            muzzleFlare.transform.position = Tip.transform.position;
            muzzleFlare.transform.rotation = Tip.transform.rotation;
            muzzleFlare.transform.SetParent(Tip.transform);
        }

        public bool CanShoot()
        {
            return _shotCooldown <= 0;
        }
    }
}