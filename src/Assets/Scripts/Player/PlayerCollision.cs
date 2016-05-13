using Assets.Scripts.Extensions;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        void OnCollisionEnter(Collision collider)
        {
            var rigidBody = GetComponent<Rigidbody>();
            var averagePosition = collider.contacts.Select(x => x.normal).Average();
//            rigidBody.AddForce(averagePosition * 250 * collider.impulse.magnitude);
        }
    }
}