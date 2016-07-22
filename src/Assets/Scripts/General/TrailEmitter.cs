using UnityEngine;

namespace Assets.Scripts.General
{
    public class TrailEmitter: MonoBehaviour
    {
        public GameObject DustGameObject;
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

            var spawnPosition = _lastPosition;
            for (var i = 0; i < particlesToSpawn; i++)
            {
                spawnPosition = Vector3.MoveTowards(spawnPosition, transform.position, SpawnDistance);
                SpawnTrail(spawnPosition);
                _lastPosition = transform.position;
            }
        }

        private void SpawnTrail(Vector3 position)
        {
            var dust = Instantiate(DustGameObject);
            dust.transform.position = position;
            dust.transform.rotation = transform.rotation;
        }
    }
}