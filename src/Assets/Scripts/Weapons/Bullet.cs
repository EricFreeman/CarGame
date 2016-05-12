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

        void OnTriggerEnter(Collider collider)
        {
            if (collider.tag != "Ground" && collider.tag != "Player")
            {
                Destroy(gameObject);
            }

            if (collider.tag == "Enemy")
            {
                Destroy(collider.gameObject);
            }
        }
    }
}