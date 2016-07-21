using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Managers
{
    public class EnemyManager : MonoBehaviour, IListener<EnemyDied>
    {
        public GameObject Enemy;

        private GameObject _player;
        private int _spawnCounter = 15;
        private int _roundCounter = 15;

        void Start()
        {
            this.Register<EnemyDied>();
            _player = GameObject.Find("Player");

            for (var i = 0; i < _roundCounter; i++)
            {
                SpawnEnemy();
            }
        }

        void OnDestroy()
        {
            this.UnRegister<EnemyDied>();
        }

        public void Handle(EnemyDied message)
        {
            _roundCounter--;
            if (_roundCounter <= 0)
            {
                _spawnCounter++;
                _roundCounter = _spawnCounter;
                SpawnEnemy();
            }

            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            var enemy = Instantiate(Enemy);
            var randomX = Random.Range(0, 2);
            var randomY = Random.Range(0, 2);
            var offset = new Vector3(Random.Range(40, 80) * (randomX == 0 ? -1 : 1), 0, Random.Range(40, 80) * (randomY == 0 ? -1 : 1));
            enemy.transform.position = _player.transform.position + offset;
            enemy.transform.SetParent(transform);
        }
    }
}