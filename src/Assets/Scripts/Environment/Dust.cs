using UnityEngine;

namespace Assets.Scripts.Environment
{
    public class Dust : MonoBehaviour
    {
        public float TimeRemaining = .1f;
        public float ScaleModifier = 1.05f;

        private float _currentTimeRemaining;
        private SpriteRenderer _spriteRenderer;

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        _currentTimeRemaining = TimeRemaining/3;
        }

        void Update()
        {
            _currentTimeRemaining -= Time.deltaTime;

            if (_currentTimeRemaining <= 0)
            {
                Destroy(gameObject);
            }
            transform.localScale *= ScaleModifier;
            _spriteRenderer.color = new Color(1f, 1f, 1f, _currentTimeRemaining / TimeRemaining);
        }
    }
}