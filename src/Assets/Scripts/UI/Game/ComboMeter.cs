using System;
using System.Collections.Generic;
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
                    for (var i = 0; i < _comboNumbers.Count; i++)
                    {
                        _comboNumbers[i].rectTransform.anchoredPosition = new Vector2(desiredStartX + i * 75, 0);
                        _comboNumbers[i].color = new Color(1, 1, 1, desiredAlpha);
                    }
                }
                else if (_animationTime > 1.5 && _animationTime < 2)
                {
                    for (var i = 0; i < _comboNumbers.Count; i++)
                    {
                        _comboNumbers[i].rectTransform.anchoredPosition = new Vector2(0 + i * 75, 0);
                        _comboNumbers[i].color = new Color(1, 1, 1, 1);
                    }
                }
                else if (_animationTime > 2f && _animationTime < 4)
                {
                    var desiredAlpha = Mathf.Lerp(1f, 0f, _animationTime - 2f);
                    var desiredX = Mathf.Lerp(0, -5000, _animationTime - 2f);
                    XImage.rectTransform.anchoredPosition = new Vector2(desiredX, 0);
                    XImage.color = new Color(1, 1, 1, desiredAlpha);

                    var desiredStartX = Mathf.Lerp(0, 5000, _animationTime - 2f);
                    for (var i = 0; i < _comboNumbers.Count; i++)
                    {
                        _comboNumbers[i].rectTransform.anchoredPosition = new Vector2(desiredStartX + i * 75, 0);
                        _comboNumbers[i].color = new Color(1, 1, 1, desiredAlpha);
                    }
                }
                else if (_animationTime > 4)
                {
                    _animationTime = 0;
                    _isDisplayingMessage = false;
                    _comboNumbers.Each(x => Destroy(x.gameObject));
                    _comboNumbers = new List<Image>(1);
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

            UpdateComboNumber(message.ComboLevel);
        }

        private void UpdateComboNumber(int comboLevel)
        {
            _comboNumbers.Each(x => Destroy(x.gameObject));
            _comboNumbers = new List<Image>();
            var bullshit = comboLevel.ToString().ToCharArray();
            for (var i = 0; i < bullshit.Length; i++)
            {
                var number = int.Parse(bullshit[i].ToString());
                var image = Instantiate(ComboNumber);
                image.sprite = ComboNumbers[number];
                image.transform.SetParent(ComboNumber.transform.parent, false);
                _comboNumbers.Add(image);
            }
        }
    }
}