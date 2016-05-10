using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Bullet : MonoBehaviour
    {
        public float BulletSpeed;

        void Update()
        {
            transform.Translate(transform.forward * BulletSpeed);
        }
    }
}