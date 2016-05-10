using System.Runtime.InteropServices;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Gun : MonoBehaviour
    {
        public GameObject BulletGameObject;
        public float ShotCooldown;

        private float _shotCooldown;

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
                bullet.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y/2, 0);
            }
        }
    }
}