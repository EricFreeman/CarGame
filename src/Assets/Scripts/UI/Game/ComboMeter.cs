using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Assets.Scripts.Messages;
using UnityEngine;
using UnityEngine.UI;
using UnityEventAggregator;

namespace Assets.Scripts.UI.Game
{
    public class ComboMeter : MonoBehaviour, IListener<NewComboLevel>
    {
        public Image XImage;
        public Image ComboNumber;
        public List<Image> ComboNumbers;

        private bool _isDisplayingMessage;
        private float _animationTime;

        void Start()
        {
            this.Register<NewComboLevel>();
        }

        void OnDestroy()
        {
            this.UnRegister<NewComboLevel>();
        }

        void Update()
        {
            if (_isDisplayingMessage)
            {
                if (Math.Abs(_animationTime) < .001f)
                {
                    GetComponent<AudioSource>().Play();
                }

                _animationTime += Time.deltaTime;

                if (_animationTime < 1.5f)
                {
                    var desiredAlpha = Mathf.Lerp(0f, 1f, _animationTime/1.5f);
                    var desiredX = Mathf.Lerp(-50, 0, _animationTime/1.5f);
                    XImage.rectTransform.anchoredPosition = new Vector2(desiredX, 0);
                    XImage.color = new Color(1, 1, 1, desiredAlpha);

                    var desiredStartX = Mathf.Lerp(50, 0, _animationTime/1.5f);
                    ComboNumber.rectTransform.anchoredPosition = new Vector2(desiredStartX, 0);
                    ComboNumber.color = new Color(1, 1, 1, desiredAlpha);
                }
                else if (_animationTime > 2f && _animationTime < 4)
                {
                    var desiredAlpha = Mathf.Lerp(1f, 0f, _animationTime - 2f);
                    var desiredX = Mathf.Lerp(0, -5000, _animationTime - 2f);
                    XImage.rectTransform.anchoredPosition = new Vector2(desiredX, 0);
                    XImage.color = new Color(1, 1, 1, desiredAlpha);

                    var desiredStartX = Mathf.Lerp(0, 5000, _animationTime - 2f);
                    ComboNumber.rectTransform.anchoredPosition = new Vector2(desiredStartX, 0);
                    ComboNumber.color = new Color(1, 1, 1, desiredAlpha);
                }
                else if (_animationTime > 4)
                {
                    _animationTime = 0;
                    _isDisplayingMessage = false;
                }
            }
        }

        public void Handle(NewComboLevel message)
        {
            if (!_isDisplayingMessage)
            {
                _animationTime = 0;
                _isDisplayingMessage = true;
            }
            else
            {
                UpdateComboNumber();
            }
        }

        private void UpdateComboNumber()
        {
            // TODO: meh
        }
    }
}