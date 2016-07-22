using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraZoom : MonoBehaviour
    {
        [HideInInspector]
        public float DefaultSize;

        private Vector3 _previousPosition;
        private UnityEngine.Camera _camera;

        void Start()
        {
            _previousPosition = transform.position;
            _camera = UnityEngine.Camera.main;
            DefaultSize = UnityEngine.Camera.main.orthographicSize;
        }

        void Update ()
        {
            var delta = Vector3.Distance(_previousPosition, transform.position);
            var currentVelocity = 0f;
            _camera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, DefaultSize + (delta * 15), ref currentVelocity, 5f * Time.deltaTime);
            
            _previousPosition = transform.position;
        }
    }
}