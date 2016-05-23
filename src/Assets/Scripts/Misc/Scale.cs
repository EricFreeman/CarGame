using UnityEngine;

namespace Assets.Scripts.Environment
{
    public class Scale : MonoBehaviour
    {
        public float TimeRemaining = .1f;

        private float _currentScale;
        private float _startScale;
        public float EndScale = 4;

        private float _currentTimeRemaining;

        void Start()
        {
            _startScale = transform.localScale.x;
            _currentScale = _startScale;
            _currentTimeRemaining = TimeRemaining;
        }

        void Update()
        {
            _currentTimeRemaining -= Time.deltaTime;

            if (_currentTimeRemaining <= 0)
            {
                Destroy(gameObject);
            }

            _currentScale = _startScale + ((EndScale - _startScale)*_currentTimeRemaining/TimeRemaining);
            transform.localScale = new Vector3(_currentScale, _currentScale, _currentScale);
        }
    }
}