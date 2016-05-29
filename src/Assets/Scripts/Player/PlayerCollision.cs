using Assets.Scripts.Extensions;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerCollision : MonoBehaviour
    {
//        void Update()
//        {
//            RaycastHit hit;
//            var rigidBody = GetComponent<Rigidbody>();
//            var ray = new Ray(transform.position, rigidBody.velocity);
//            Debug.DrawRay(transform.position, rigidBody.velocity);
//
//            if (Physics.Raycast(ray, out hit))
//            {
//                if (hit.collider.tag == "Wall")
//                {
//                    Bounce(hit.normal);
//                    Debug.Log(hit.collider.tag);
//                }
//                else
//                {
//                    Debug.Log(hit.collider.tag);
//                }
//            }
//        }

//        void OnCollisionEnter(Collision collider)
//        {
//            if (collider.transform.tag == "Wall")
//            {
//                Bounce(collider.contacts.Select(x => x.normal).Average());
//            }
//        }
//
//        private void Bounce(Vector3 normal)
//        {
//            var rigidBody = GetComponent<Rigidbody>();
//            var velocity = rigidBody.velocity;
//
//            rigidBody.velocity = -(2f*Vector3.Scale(Vector3.Scale(normal, velocity), normal) - velocity);
//        }
    }
}