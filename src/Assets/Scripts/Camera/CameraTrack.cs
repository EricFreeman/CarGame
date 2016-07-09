using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraTrack : MonoBehaviour
    {
        public GameObject GameObject;
        public Vector3 Offset;

        public float SmoothTime = 0.3f; 
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _lastPosition;

        void Start()
        {
            transform.position = GameObject.transform.position + Offset;
            _lastPosition = GameObject.transform.position;
        }

        void LateUpdate()
        {
            var currentPosition = GameObject.transform.position;
            var delta = currentPosition - _lastPosition;

            var goalPos = GameObject.transform.position + (delta * 30f);
            //var goalPos = GameObject.transform.position + (GameObject.transform.forward * delta.magnitude * 20f);
            goalPos.y = transform.position.y;
            
            transform.position = Vector3.SmoothDamp(transform.position, goalPos, ref _velocity, SmoothTime);

            _lastPosition = GameObject.transform.position;
        }
    }
}