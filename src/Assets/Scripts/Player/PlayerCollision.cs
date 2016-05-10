using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        void OnCollisionEnter(Collision collider)
        {
//            var rigidBody = GetComponent<Rigidbody>();
//            foreach (var contactPoint in collider.contacts)
//            {
//                rigidBody.AddForce(contactPoint.normal * 10 * collider.impulse.magnitude);
//            }
        }
    }
}