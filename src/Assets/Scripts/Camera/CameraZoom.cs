using Assets.Scripts.Environment;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraZoom : MonoBehaviour
    {
        [HideInInspector]
        public float DefaultSize;

        private Vector3 _previousPosition;
        private UnityEngine.Camera _camera;

        private Vector3 _lastEverythingPosition;

        void Start()
        {
            _previousPosition = transform.position;
            _camera = UnityEngine.Camera.main;
            DefaultSize = UnityEngine.Camera.main.orthographicSize;
        }

        void Update ()
        {
            var negatedDelta = (Hack.Everything.transform.position - _lastEverythingPosition).magnitude;
            var delta = Mathf.Abs(Vector3.Distance(_previousPosition, transform.position) - negatedDelta);

            var currentVelocity = 0f;
            _camera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, DefaultSize + (delta * 15), ref currentVelocity, 5f * Time.deltaTime);
            
            _previousPosition = transform.position;
            _lastEverythingPosition = Hack.Everything.transform.position;
        }
    }
}