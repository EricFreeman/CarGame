using UnityEngine;

namespace Assets.Scripts.Environment
{
    public class EndlessScrollHack : MonoBehaviour
    {
        private GameObject _player;

        void Start()
        {
            _player = GameObject.Find("Player");
            Hack.Everything = GameObject.Find("Everything");
        }

        void Update()
        {
            if (Vector3.Distance(_player.transform.position, transform.position) > 250)
            {
                var bullshit = _player.transform.position;
                bullshit = new Vector3(bullshit.x, 0, bullshit.z);
                Hack.Everything.transform.Translate(bullshit.normalized*-250);
            }
        }
    }

    public static class Hack
    {
        public static GameObject Everything;
    }
}