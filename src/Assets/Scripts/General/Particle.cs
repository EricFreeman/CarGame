using UnityEngine;

namespace Assets.Scripts.General
{
    public class Particle : MonoBehaviour
    {
        [HideInInspector]
        public Vector3 Velocity;

        public float Friction = .95f;
        public float Cutoff = .001f;
        public bool DestroyOnRest = true;

        private bool _isResting;

        void Update()
        {
            if (_isResting)
            {
                return;
            }

            Velocity.y = 0;

            transform.Translate(Velocity * Time.deltaTime);
            Velocity *= Friction;

            if(Velocity.magnitude < Cutoff && DestroyOnRest)
            {
                _isResting = true;
                Destroy(GetComponent<Particle>());
            }
        }
    }
}