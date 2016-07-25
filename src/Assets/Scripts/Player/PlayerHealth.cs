using System;
using System.Collections.Generic;
using Assets.Scripts.Enemies;
using Assets.Scripts.General;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEventAggregator;

namespace Assets.Scripts.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public int MaxHealth = 8;
        public Image HealthImage;
        public List<Sprite> HealthSprites;

        [HideInInspector]
        public int CurrentHealth;

        public GameObject PlayerBody;

        private bool _isDead;
        private Rigidbody _rigidBody;

        void Start()
        {
            CurrentHealth = MaxHealth;
            _rigidBody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            var percent = (double)CurrentHealth/MaxHealth;

            var frame = (int)(HealthSprites.Count - percent * HealthSprites.Count);
            HealthImage.sprite = CurrentHealth > 0 ? HealthSprites[frame] : null;

            if (_isDead)
            {
                HealthImage.color = new Color(0, 0, 0, 0);
                if (Input.GetKey(KeyCode.R))
                {
                    SceneManager.LoadScene("Game");
                }

                _rigidBody.velocity *= .99f;
                if (_rigidBody.velocity.magnitude < 0.001f)
                {
                    _rigidBody.velocity = Vector3.zero;
                }
            }
        }

        void OnTriggerEnter(Collider collider)
        {
            if (_isDead)
            {
                return;
            }

            var enemy = collider.GetComponent<EnemyHealthBehavior>();
            if (enemy != null)
            {
                enemy.Die(new DamageContext());
                CurrentHealth--;
                if (CurrentHealth <= 0)
                {
                    Die();
                }
            }
        }

        public void Die()
        {
            _isDead = true;
            var playerMovement = gameObject.GetComponent<PlayerMovement>();
            _rigidBody.isKinematic = false;
            _rigidBody.velocity = playerMovement.Movement * 15;

            Destroy(playerMovement);
            Destroy(PlayerBody);
            Destroy(GetComponentInChildren<Boost>());
            Destroy(GetComponent<NavMeshObstacle>());
            Destroy(GetComponentInChildren<PlayerWeapons>());
            GetComponentsInChildren<PlayerWheels>().Each(Destroy);
        }
    }
}