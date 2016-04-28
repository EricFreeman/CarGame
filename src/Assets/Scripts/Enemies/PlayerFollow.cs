using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class PlayerFollow : MonoBehaviour
    {
        public GameObject Player;

        void Update()
        {
            GetComponent<NavMeshAgent>().destination = Player.transform.position;
        }
    }
}