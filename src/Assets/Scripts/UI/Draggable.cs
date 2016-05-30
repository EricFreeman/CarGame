using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public Transform Target;
        public bool ShouldReturn;

        private bool _isMouseDown = false;
        private Vector3 _startMousePosition;
        private Vector3 _startPosition;

        public void OnPointerDown(PointerEventData dt)
        {
            _isMouseDown = true;

            Debug.Log("Draggable Mouse Down");

            _startPosition = Target.position;
            _startMousePosition = Input.mousePosition;
        }

        public void OnPointerUp(PointerEventData dt)
        {
            _isMouseDown = false;

            if (ShouldReturn)
            {
                Target.position = _startPosition;
            }
        }

        void Update()
        {
            if (_isMouseDown)
            {
                var currentPosition = Input.mousePosition;
                var diff = currentPosition - _startMousePosition;
                var pos = _startPosition + diff;
                Target.position = pos;
            }
        }
    }
}