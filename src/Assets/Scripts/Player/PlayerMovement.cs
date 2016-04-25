using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public float Acceleration = 1f;
        public float GroundFriction = .001f;
        public float MagnitudeCutoff = .005f;
        public float TurnSpeed = 30f;
        
        public Vector3 Movement;


        void Update()
        {
            ApplyFriction();
            ApplyMovement();
        }

        private void ApplyMovement()
        {
            var horizontal = Input.GetAxisRaw("Horizontal") * Movement.magnitude * TurnSpeed;
            var veritcal = Input.GetAxisRaw("Vertical");

            transform.Rotate(new Vector3(0, horizontal, 0));
            var newMovement = transform.forward*Acceleration*veritcal;
            Movement += newMovement;

            transform.position += Movement;
        }

        private void ApplyFriction()
        {
            Movement *= GroundFriction;
            if (Movement.magnitude < MagnitudeCutoff)
            {
                Movement = Vector3.zero;
            }
        }
    }
}