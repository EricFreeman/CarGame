using System.Linq;
using Assets.Scripts.Extensions;
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

            transform.Rotate(new Vector3(0, horizontal * Time.timeScale, 0));
            var newMovement = transform.forward*Acceleration*veritcal;

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