using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraTrack : MonoBehaviour
    {
        public GameObject GameObject;
        public Vector3 Offset;

        public float SmoothTime = 0.3f; 
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _lastGameObjectPosition;
        private Vector3 _lastPosition;

        void Start()
        {
            transform.position = GameObject.transform.position + Offset;
            _lastGameObjectPosition = GameObject.transform.position;
        }

        void LateUpdate()
        {
            var negatedDelta = transform.position - _lastPosition;
            var delta = GameObject.transform.position - _lastGameObjectPosition - negatedDelta;

            var goalPos = GameObject.transform.position + (GameObject.transform.forward * delta.magnitude * 20f);
            goalPos.y = transform.position.y;
            
            transform.position = Vector3.SmoothDamp(transform.position, goalPos, ref _velocity, SmoothTime);

            _lastGameObjectPosition = GameObject.transform.position;
            _lastPosition = transform.position;
        }
    }
}