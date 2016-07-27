using System.Collections.Generic;
using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;
using UnityEventAggregator;

namespace Assets.Scripts.UI.Game
{
    public class ScoreNumber : MonoBehaviour
    {
        public Image Number;
        public List<Sprite> Images;
        public ScoreManager ScoreManager;
        public int Spacing;
        public int PaddingX;
        public int PaddingY;

        private List<Image> _numbers;
        private int _lastScore;

        void Start()
        {
            UpdateScore(0);
        }

        void Update()
        {
            if (_lastScore != ScoreManager.Score)
            {
                UpdateScore(ScoreManager.Score);
            }

            _lastScore = ScoreManager.Score;
        }

        private void UpdateScore(int score)
        {
            _numbers.Each(x => Destroy(x.gameObject));
            _numbers = new List<Image>();
            var charArray = score.ToString().ToCharArray();
            for (var i = 0; i < charArray.Length; i++)
            {
                var number = int.Parse(charArray[i].ToString());
                var image = Instantiate(Number);
                image.sprite = Images[number];
                image.transform.SetParent(transform, false);
                image.rectTransform.anchoredPosition = new Vector2(PaddingX + i * Spacing, PaddingY);
                _numbers.Add(image);
            }
        }
    }
}
