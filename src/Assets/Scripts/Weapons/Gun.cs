using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Gun : MonoBehaviour
    {
        public GameObject BulletGameObject;
        public AudioClip BulletSound;
        public float ShotCooldown;
        public float Accuracy;

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
            if (_shotCooldown <= 0)
            {
                _shotCooldown = ShotCooldown;
                var bullet = Instantiate(BulletGameObject);
                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y/2 + Random.Range(-Accuracy, Accuracy), 0);
                _audioSource.PlayOneShot(BulletSound);
                _audioSource.pitch = 1 + Random.Range(-_pitchRandom, _pitchRandom);
                _audioSource.volume = 1 - Random.Range(0, _volumeRandom);
            }
        }
    }
}