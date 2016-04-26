using Assets.Scripts.Environment;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class DustEmitter : MonoBehaviour
    {
        public Dust DustGameObject;
        public float SpawnDistance = .2f;

        private Vector3 _lastPosition;

        void Start()
        {
            _lastPosition = transform.position;
        }

        void Update()
        {
            var delta = transform.position - _lastPosition;
            var particlesToSpawn = (int)(delta.magnitude/SpawnDistance);

            Vector3 spawnPosition = _lastPosition;
            for (var i = 0; i < particlesToSpawn; i++)
            {
                spawnPosition = Vector3.MoveTowards(spawnPosition, transform.position, SpawnDistance);
                SpawnDust(spawnPosition);
                _lastPosition = transform.position;
            }

        }

        private void SpawnDust(Vector3 position)
        {
            var dust = Instantiate(DustGameObject);
            dust.transform.position = position;
        }
    }
}