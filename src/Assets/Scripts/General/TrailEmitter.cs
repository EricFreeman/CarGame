using Assets.Scripts.Environment;
using UnityEngine;

namespace Assets.Scripts.General
{
    public class TrailEmitter: MonoBehaviour
    {
        public GameObject DustGameObject;
        public float SpawnDistance = .2f;

        private Vector3 _lastPosition;
        private Vector3 _lastEverythingPosition;

        void Start()
        {
            _lastPosition = transform.position;
            _lastEverythingPosition = Hack.Everything.transform.position;
        }

        void Update()
        {
            var negatedDelta = (Hack.Everything.transform.position - _lastEverythingPosition);
            var delta = transform.position - _lastPosition - negatedDelta;
            var particlesToSpawn = (int)(delta.magnitude/SpawnDistance);

            var spawnPosition = _lastPosition;
            for (var i = 0; i < particlesToSpawn; i++)
            {
                spawnPosition = Vector3.MoveTowards(spawnPosition, transform.position, SpawnDistance);
                SpawnTrail(spawnPosition);
                _lastPosition = transform.position;
            }

            _lastEverythingPosition = Hack.Everything.transform.position;
        }

        private void SpawnTrail(Vector3 position)
        {
            var dust = Instantiate(DustGameObject);
            dust.transform.position = position;
            dust.transform.rotation = transform.rotation;
            dust.transform.SetParent(Hack.Everything.transform);
        }
    }
}