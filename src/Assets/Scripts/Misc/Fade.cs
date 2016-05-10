﻿using UnityEngine;

namespace Assets.Scripts.Misc
{
    public class Fade : MonoBehaviour
    {
        public float TimeRemaining = .1f;

        private float _currentTimeRemaining;
        private SpriteRenderer _spriteRenderer;

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _currentTimeRemaining = TimeRemaining;
        }

        void Update()
        {
            _currentTimeRemaining -= Time.deltaTime;

            if (_currentTimeRemaining <= 0)
            {
                Destroy(gameObject);
            }

            _spriteRenderer.color = new Color(1f, 1f, 1f, _currentTimeRemaining / TimeRemaining);
        }
    }
}