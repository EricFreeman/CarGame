using Assets.Scripts.Environment;
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
            enemy.transform.SetParent(Hack.Everything.transform);
            var offset = SpawnOffScreen();
            enemy.transform.position = _player.transform.position + offset;
            enemy.transform.SetParent(transform);
        }

        public static Vector3 SpawnOffScreen()
        {
            var position = Random.Range(0, 4);
            if (position == 0)
            {
                return new Vector3(Random.Range(-120f, 120f), 0, Random.Range(100f, 120f));
            }
            else if (position == 1)
            {
                return new Vector3(Random.Range(100f, 120f), 0, Random.Range(-120f, 120f));
            }
            else if (position == 2)
            {
                return new Vector3(Random.Range(-120f, 120f), 0, Random.Range(-100f, -120f));
            }
            else
            {
                return new Vector3(Random.Range(-100f, -120f), 0, Random.Range(-120f, 120f));
            }
        }
    }
}