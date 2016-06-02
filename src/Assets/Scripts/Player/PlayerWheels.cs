using System.Net.Sockets;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerWheels : MonoBehaviour
    {
        public float MaxRotation = 30f;
        public float TurnSpeed = 100f;

        void Update()
        {
            var turnAmount = Input.GetAxisRaw("Horizontal");

            if (turnAmount < 0)
            {
                transform.localRotation = Quaternion.Euler(0, Mathf.MoveTowardsAngle(transform.localEulerAngles.y, -MaxRotation, TurnSpeed * Time.deltaTime), 0);
            }
            else if (turnAmount > 0)
            {
                transform.localRotation = Quaternion.Euler(0, Mathf.MoveTowardsAngle(transform.localEulerAngles.y, MaxRotation, TurnSpeed * Time.deltaTime), 0);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, Mathf.MoveTowardsAngle(transform.localEulerAngles.y, 0, TurnSpeed * Time.deltaTime), 0);
            }
        }
    }
}