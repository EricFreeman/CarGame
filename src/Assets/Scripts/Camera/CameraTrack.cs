using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraTrack : MonoBehaviour
    {
        public GameObject GameObject;
        public Vector3 Offset;
        public float SmoothTime = 0.3f;

        private Vector3 _velocity = Vector3.zero;

        void Start()
        {
            transform.position = GameObject.transform.position + Offset;
        }

        void Update()
        {
            var goalPos = GameObject.transform.position;
            goalPos.y = transform.position.y;
            transform.position = Vector3.SmoothDamp(transform.position, goalPos, ref _velocity, SmoothTime);
        }
    }
}