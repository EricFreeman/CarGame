using UnityEngine;

namespace Assets.Scripts.General
{
    public class Fade : MonoBehaviour
    {
        public float TimeRemaining = .1f;
        public float CurrentTimeOverride = -1;
        public float FadeSpeed = 1f;

        private float _currentTimeRemaining;
        private SpriteRenderer _spriteRenderer;

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _currentTimeRemaining = CurrentTimeOverride >= 0 ? CurrentTimeOverride : TimeRemaining;

            var color = _spriteRenderer.color;
            _spriteRenderer.color = new Color(color.r, color.g, color.b, _currentTimeRemaining / TimeRemaining);
        }

        void Update()
        {
            _currentTimeRemaining -= (Time.deltaTime * FadeSpeed);

            if (_currentTimeRemaining <= 0)
            {
                Destroy(gameObject);
            }

            var color = _spriteRenderer.color;
            _spriteRenderer.color = new Color(color.r, color.g, color.b, _currentTimeRemaining / TimeRemaining);
        }
    }
}