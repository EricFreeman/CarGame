using Assets.Scripts.Messages;
using Assets.Scripts.UI.Game;
using UnityEngine;
using UnityEngine.UI;
using UnityEventAggregator;

namespace Assets.Scripts.Managers
{
    public class ScoreManager : MonoBehaviour, IListener<EnemyDied>
    {
        public float ComboTime = 5f;
        private float _currentComboTime;

        public int KillsPerComboLevel = 5;
        private int _currentCombo;
        private int _currentKills;

        public ScoreNumber ScoreNumber;
        public ScoreNumber ComboNumber;

        [HideInInspector]
        public int Score;

        void Start()
        {
            this.Register<EnemyDied>();
            ScoreNumber.UpdateScore(0);
        }

        void OnDestroy()
        {
            this.UnRegister<EnemyDied>();
        }

        void Update()
        {
            _currentComboTime -= Time.deltaTime;

            if (_currentCombo > 0)
            {
                ComboNumber.GetComponentsInChildren<Image>()
                    .Each(x => x.color = new Color(1, 1, 1, _currentComboTime/ComboTime));
            }

            if (_currentComboTime <= 0 && _currentCombo > 0)
            {
                _currentCombo = 0;
                _currentKills = 0;
                EventAggregator.SendMessage(new LostCombo());
            }
        }

        public void Handle(EnemyDied message)
        {
            UpdateCombo();
        }

        private void UpdateCombo()
        {
            _currentComboTime = ComboTime;
            _currentKills++;

            if (_currentKills >= KillsPerComboLevel)
            {
                _currentKills = 0;
                _currentCombo++;
                EventAggregator.SendMessage(new NewComboLevel { ComboLevel = _currentCombo });
            }

            AddPoints(1);
        }

        private void AddPoints(int points)
        {
            Score += points + (points * _currentCombo);
            ScoreNumber.UpdateScore(Score);
        }
    }
}