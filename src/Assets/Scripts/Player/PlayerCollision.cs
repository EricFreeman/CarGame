using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        void OnCollisionEnter(Collision collider)
        {
            var rigidBody = GetComponent<Rigidbody>();
            rigidBody.AddForce(collider.relativeVelocity * 100);
            Debug.Log("shit");
        }
    }
}