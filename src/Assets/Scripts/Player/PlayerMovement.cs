using System.Linq;
using Assets.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public float Acceleration = .02f;
        public float GroundFriction = .99f;
        public float MagnitudeCutoff = .001f;
        public float BoostTurnSpeed = 3f;
        public float SlideTurnSpeed = 15f;
        public float MaxSpeed = .5f;
        
        public Vector3 Movement;

        void Update()
        {
            ApplyFriction();
            ApplyMovement();
        }

        private void ApplyMovement()
        {
            var veritcal = Input.GetAxisRaw("Vertical");

            if (veritcal < 0)
            {
                Movement *= .9f;
            }
            else
            {
                var horizontal = Input.GetAxisRaw("Horizontal") * Movement.magnitude * (veritcal > .01f ? BoostTurnSpeed : SlideTurnSpeed) * Time.timeScale;

                transform.Rotate(new Vector3(0, horizontal, 0));
                var newMovement = transform.forward * Acceleration * veritcal;

                RaycastHit hit;
                var rayTest = Movement + newMovement;
                var ray = new Ray(transform.position, rayTest.normalized * 2);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.distance <= rayTest.magnitude + 1.5f)
                    {
                        Bounce(hit.normal);
                    }
                }
                Movement += newMovement;

                if (Movement.magnitude > MaxSpeed)
                {
                    Movement = Movement.normalized * MaxSpeed;
                }
            }

            transform.position += Movement * Time.timeScale;
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Wall")
            {
                Bounce(collision.contacts.Select(x => x.normal).Average());
            }
        }

        private void Bounce(Vector3 normal)
        {
            Movement = -(2f * Vector3.Scale(Vector3.Scale(normal, Movement), normal) - Movement) * .2f;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Movement), .1f);
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