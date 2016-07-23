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
                var offset = _player.GetComponent<PlayerMovement>().Movement.normalized * 350;
                offset += new Vector3(Random.Range(-_repsawnRange, _repsawnRange), 0, Random.Range(-_repsawnRange, _repsawnRange));
                var respawnAround = _player.transform.position + offset;
                GetComponent<NavMeshAgent>().enabled = false;
                transform.position = respawnAround;
                GetComponent<NavMeshAgent>().enabled = true;;

                Debug.Log("offset: " + offset);
                Debug.Log("enemy: " + transform.position);
                Debug.Log("player: " + _player.transform.position);
                Debug.Log("distance: " + Vector3.Distance(transform.position, _player.transform.position));
                if (Vector3.Distance(transform.position, _player.transform.position) < 100)
                {
                    Debug.Log("respawn around: " + respawnAround);
                    Debug.Log("okay fuck");
                }

                _respawnTime = Random.Range(1f, 10f);
                _currentChangeOffsetTime = 0;
            }
        }
    }
}