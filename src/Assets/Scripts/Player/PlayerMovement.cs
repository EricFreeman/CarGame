using System;
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

        public AudioClip EngineLoop;
        public AudioClip EngineGas;

        private AudioSource _engineGas;
        private AudioSource _engineLoop;

        void Start()
        {
            _engineGas = gameObject.AddComponent<AudioSource>();
            _engineGas.clip = EngineGas;

            _engineLoop = gameObject.AddComponent<AudioSource>();
            _engineLoop.clip = EngineLoop;
            _engineLoop.loop = true;
        }

        void Update()
        {
            ApplyFriction();
            ApplyMovement();
        }

        private void ApplyMovement()
        {
            var vertical = Input.GetAxisRaw("Vertical");

            if (vertical < 0)
            {
                Movement *= .9f;  
            }
            else
            {
                if (Math.Abs(vertical) < .1f)
                {
                    _engineLoop.Stop();
                    _engineGas.Stop();
                    _engineLoop.pitch = .75f;
                }

                var horizontal = Input.GetAxisRaw("Horizontal") * Movement.magnitude * (vertical > .01f ? BoostTurnSpeed : SlideTurnSpeed) * Time.timeScale;

                transform.Rotate(new Vector3(0, horizontal, 0));
                var newMovement = transform.forward * Acceleration * vertical;

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

                PlayEngineNoises(newMovement.magnitude);

                if (Movement.magnitude > MaxSpeed)
                {
                    Movement = Movement.normalized * MaxSpeed;
                }
            }

            transform.position += Movement * Time.timeScale;
        }

        private float _previousMovement;
        private void PlayEngineNoises(float newMovement)
        {
            if (newMovement > 0)
            {
                _engineLoop.pitch = Mathf.Min(_engineLoop.pitch + newMovement, 1);
                if (!_engineLoop.isPlaying)
                {
                    _engineLoop.Play();
                }

                if (Math.Abs(_previousMovement) < .01f && newMovement > .01f)
                {
                    _engineGas.Play();
                }
            }

            _previousMovement = newMovement;
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