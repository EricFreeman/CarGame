using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ResizeBasedOnChildren : MonoBehaviour
    {
        private List<RectTransform> _children;
        private RectTransform _rectTransform;

        private bool _hasRun;

        void Start()
        {
            _children = GetComponentsInChildren<RectTransform>().ToList();
            _rectTransform = transform as RectTransform;
            _children.Remove(_children.First(x => x.GetInstanceID() == transform.GetInstanceID()));
        }

        void Update()
        {
            var currentHeight = _rectTransform.rect.height;
            if (!_hasRun && currentHeight > 0)
            {
                _hasRun = true;
                var childrenSize = _children.Sum(x => LayoutUtility.GetPreferredHeight(x));
                _rectTransform.sizeDelta = new Vector2(0, childrenSize - currentHeight + 96);
            }
        }
    }
}