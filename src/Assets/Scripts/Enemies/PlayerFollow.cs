using Assets.Scripts.Managers;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class PlayerFollow : MonoBehaviour
    {
        private GameObject _player;
        public float MaxChangeOffsetTime = 5f;
        public float MaxOffsetAmount = 10f;

        private Vector3 _offset;
        private float _currentChangeOffsetTime;
        private float _respawnTime;
        private const float _repsawnRange = 150f;

        void Start()
        {
            _currentChangeOffsetTime = Random.Range(1, MaxChangeOffsetTime);
            _player = GameObject.Find("Player");
        }

        void LateUpdate()
        {
            _currentChangeOffsetTime -= Time.deltaTime;

            if (_currentChangeOffsetTime <= 0)
            {
                _currentChangeOffsetTime = Random.Range(1, MaxChangeOffsetTime);
                _offset = new Vector3(Random.Range(-MaxOffsetAmount, MaxOffsetAmount), 0, Random.Range(-MaxOffsetAmount, MaxOffsetAmount));
            }

            GetComponent<NavMeshAgent>().destination = _player.transform.position + _offset;

            RepositionEnemy();
        }

        private void RepositionEnemy()
        {
            _respawnTime -= Time.deltaTime;
            var distance = Vector3.Distance(_player.transform.position, transform.position);
            if (distance > 125 && _respawnTime <= 0)
            {
                var newPos = GetNewPosition();
                
                GetComponent<NavMeshAgent>().enabled = false;
                transform.position = newPos;
                GetComponent<NavMeshAgent>().enabled = true;;

                _respawnTime = Random.Range(1f, 10f);
                _currentChangeOffsetTime = 0;
            }
        }

        private Vector3 GetNewPosition()
        {
            var respawnType = Random.Range(0, 2);

            if (respawnType == 0)
            {
                var offset = EnemyManager.SpawnOffScreen();
                return _player.transform.position + offset;
            }
            else
            {
                var movement = _player.GetComponent<PlayerMovement>();
                Vector3 offset;
                if (movement != null)
                {
                    offset = _player.GetComponent<PlayerMovement>().Movement.normalized*350;
                }
                else
                {
                    offset = Vector3.zero;
                }

                offset += new Vector3(Random.Range(-_repsawnRange, _repsawnRange), 0,
                    Random.Range(-_repsawnRange, _repsawnRange));
                return _player.transform.position + offset;
            }
        }
    }
}