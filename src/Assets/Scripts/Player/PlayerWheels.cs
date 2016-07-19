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
            var desiredAngle = 0f;

            if (turnAmount < 0)
            {
                desiredAngle = -MaxRotation;
            }
            else if (turnAmount > 0)
            {
                desiredAngle = MaxRotation;
            }

            transform.localRotation = Quaternion.Euler(0, Mathf.MoveTowardsAngle(transform.localEulerAngles.y, desiredAngle, TurnSpeed * Time.deltaTime), 0);
        }
    }
}