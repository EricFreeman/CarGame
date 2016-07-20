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
        }

        void Update ()
        {
            if (Time.fixedTime < 1)
            {
                DefaultSize = UnityEngine.Camera.main.orthographicSize;
            }
            else
            {
                var delta = Vector3.Distance(_previousPosition, transform.position);
                var currentVelocity = 0f;
                _camera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, DefaultSize + (delta * 10), ref currentVelocity, 5f * Time.deltaTime);
                
                _previousPosition = transform.position;
            }
        }
    }
}