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
            if (Hack.Everything != null)
            {
                _lastEverythingPosition = Hack.Everything.transform.position;
            }
        }

        void LateUpdate()
        {
            var negatedDelta = Hack.Everything.transform.position - _lastEverythingPosition;
            var delta = transform.position - _lastPosition - negatedDelta;
            var particlesToSpawn = (int)(delta.magnitude/SpawnDistance);

            // this is a hack because I don't feel like figuring out why this is happening
            if (particlesToSpawn > 20)
            {
                particlesToSpawn = 1;
            }

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