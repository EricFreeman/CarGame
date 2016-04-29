using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class PlayerFollow : MonoBehaviour
    {
        public GameObject Player;
        public float MaxChangeOffsetTime = 5f;
        public float MaxOffsetAmount = 10f;

        private Vector3 _offset;
        private float _currentChangeOffsetTime;

        void Start()
        {
            _currentChangeOffsetTime = Random.Range(1, MaxChangeOffsetTime);
        }

        void Update()
        {
            _currentChangeOffsetTime -= Time.deltaTime;

            if (_currentChangeOffsetTime <= 0)
            {
                _currentChangeOffsetTime = Random.Range(1, MaxChangeOffsetTime);
                _offset = new Vector3(Random.Range(-MaxOffsetAmount, MaxOffsetAmount), 0, Random.Range(-MaxOffsetAmount, MaxOffsetAmount));
            }

            GetComponent<NavMeshAgent>().destination = Player.transform.position + _offset;
        }
    }
}