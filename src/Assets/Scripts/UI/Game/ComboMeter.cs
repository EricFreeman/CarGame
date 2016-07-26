﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        public List<Sprite> ComboNumbers;

        private bool _isDisplayingMessage;
        private float _animationTime;
        private List<Image> _comboNumbers;

        void Start()
        {
            this.Register<NewComboLevel>();
            _comboNumbers = new List<Image>();

            _isDisplayingMessage = true;
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

                UpdateAnimation();
            }
        }

        #region Animations

        private float StayStartTime = .25f;
        private float ExitStartTime = 1f;
        private float CleanupStartTime = 2.5f;
        private float LetterSize = 35f;
        private float DesiredY = -150;

        private void UpdateAnimation()
        {
            if (_animationTime < StayStartTime)
            {
                Enter();
            }
            else if (_animationTime > StayStartTime && _animationTime < ExitStartTime)
            {
                Stay();
            }
            else if (_animationTime > ExitStartTime && _animationTime < CleanupStartTime)
            {
                Exit();
            }
            else if (_animationTime > CleanupStartTime)
            {
                CleanUp();
            }
        }

        private void Enter()
        {
            var desiredAlpha = Mathf.Lerp(0f, 1f, _animationTime / StayStartTime);
            var desiredX = Mathf.Lerp(-250, 0, _animationTime / StayStartTime);
            XImage.rectTransform.anchoredPosition = new Vector2(desiredX, DesiredY);
            XImage.color = new Color(1, 1, 1, desiredAlpha);

            var desiredStartX = Mathf.Lerp(250, 0, _animationTime / StayStartTime);
            for (var i = 0; i < _comboNumbers.Count; i++)
            {
                _comboNumbers[i].rectTransform.anchoredPosition = new Vector2(desiredStartX + i * LetterSize, DesiredY);
                _comboNumbers[i].color = new Color(1, 1, 1, desiredAlpha);
            }
        }

        private void Stay()
        {
            for (var i = 0; i < _comboNumbers.Count; i++)
            {
                _comboNumbers[i].rectTransform.anchoredPosition = new Vector2(0 + i * LetterSize, DesiredY);
                _comboNumbers[i].color = new Color(1, 1, 1, 1);
            }
        }

        private void Exit()
        {
            var desiredAlpha = Mathf.Lerp(1f, 0f, _animationTime - ExitStartTime);
            var desiredX = Mathf.Lerp(0, -5000, _animationTime - ExitStartTime);
            XImage.rectTransform.anchoredPosition = new Vector2(desiredX, DesiredY);
            XImage.color = new Color(1, 1, 1, desiredAlpha);

            var desiredStartX = Mathf.Lerp(0, 5000, _animationTime - ExitStartTime);
            for (var i = 0; i < _comboNumbers.Count; i++)
            {
                _comboNumbers[i].rectTransform.anchoredPosition = new Vector2(desiredStartX + i * LetterSize, DesiredY);
                _comboNumbers[i].color = new Color(1, 1, 1, desiredAlpha);
            }
        }

        private void CleanUp()
        {
            _animationTime = 0;
            _isDisplayingMessage = false;
            _comboNumbers.Each(x => Destroy(x.gameObject));
            _comboNumbers = new List<Image>(1);
        }

        #endregion

        public void Handle(NewComboLevel message)
        {
            if (!_isDisplayingMessage)
            {
                _animationTime = 0;
                _isDisplayingMessage = true;
            }

            UpdateComboNumber(message.ComboLevel);
        }

        private void UpdateComboNumber(int comboLevel)
        {
            _comboNumbers.Each(x => Destroy(x.gameObject));
            _comboNumbers = new List<Image>();
            var charArray = comboLevel.ToString().ToCharArray();
            foreach (var num in charArray)
            {
                var number = int.Parse(num.ToString());
                var image = Instantiate(ComboNumber);
                image.sprite = ComboNumbers[number];
                image.transform.SetParent(ComboNumber.transform.parent, false);
                _comboNumbers.Add(image);
            }
        }
    }
}