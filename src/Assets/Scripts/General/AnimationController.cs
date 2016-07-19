using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.General
{
    public class AnimationController : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer;

        private List<Sprite> _currentAnimation;
        private AnimationType _animationType;

        private List<Sprite> _previousAnimation;
        private AnimationType? _previousAnimationType;

        public float FramesPerSeconds;

        private int _currentFrame;
        private float _timeOnFrame;

        void Update()
        {
            if (_currentAnimation == null) return;

            _timeOnFrame += Time.deltaTime;
            if (_timeOnFrame >= 1.0 / FramesPerSeconds)
            {
                _timeOnFrame = 0;
                _currentFrame++;

                if (_currentFrame >= _currentAnimation.Count)
                {
                    if (_animationType == AnimationType.DestroyWhenComplete)
                    {
                        Destroy(gameObject);
                        return;
                    }
                    else if (_animationType == AnimationType.OneOff)
                    {
                        _animationType = AnimationType.Loop;
                        _currentAnimation = _previousAnimation;
                        _previousAnimation = null;
                        _previousAnimationType = null;
                    }

                    _currentFrame = 0;
                }
            }

            if (_currentAnimation != null)
            {
                SpriteRenderer.sprite = _currentAnimation[_currentFrame];
            }
        }

        public void PlayAnimation(List<Sprite> anim, AnimationType animationType)
        {
            if (animationType == AnimationType.OneOff && _previousAnimationType == AnimationType.OneOff)
            {
                return;
            }

            if (animationType == AnimationType.OneOff)
            {
                _previousAnimation = _currentAnimation;
            }

            _animationType = animationType;
            _previousAnimationType = _animationType;

            _currentAnimation = anim;
            _currentFrame = 0;
        }
    }
}