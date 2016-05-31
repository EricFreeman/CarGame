using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class Resizable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public RectTransform Target;
        public Vector2 MinSize;

        private bool _isMouseDown;
        private Vector3 _lastPosition;

        public void OnPointerDown(PointerEventData dt)
        {
            _isMouseDown = true;
            _lastPosition = Input.mousePosition;
        }

        public void OnPointerUp(PointerEventData dt)
        {
            _isMouseDown = false;
        }

        void Update()
        {
            if (_isMouseDown)
            {
                var currentPosition = Input.mousePosition;
                var diff = currentPosition - _lastPosition;
                _lastPosition = currentPosition;

                Target.sizeDelta = new Vector2(Target.sizeDelta.x + diff.x, Target.sizeDelta.y - diff.y);

                if (Target.sizeDelta.x < MinSize.x)
                {
                    Target.sizeDelta = new Vector2(MinSize.x, Target.sizeDelta.y);
                }

                if (Target.sizeDelta.y < MinSize.y)
                {
                    Target.sizeDelta = new Vector2(Target.sizeDelta.x, MinSize.y);
                }

                Canvas.ForceUpdateCanvases();
            }
        }
    }
}