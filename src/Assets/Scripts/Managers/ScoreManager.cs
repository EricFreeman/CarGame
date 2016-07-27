using Assets.Scripts.Messages;
using UnityEngine;
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

        [HideInInspector]
        public int Score;

        void Start()
        {
            this.Register<EnemyDied>();
        }

        void OnDestroy()
        {
            this.UnRegister<EnemyDied>();
        }

        void Update()
        {
            _currentComboTime -= Time.deltaTime;

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
        }
    }
}