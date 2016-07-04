using UnityEngine;

public class Particle : MonoBehaviour
{
    [HideInInspector]
    public Vector3 Velocity;

    public float Friction = .95f;
    public float Cutoff = .001f;

    void Update()
    {
        Velocity.y = 0;

        transform.Translate(Velocity * Time.deltaTime);
        Velocity *= Friction;

        if(Velocity.magnitude < Cutoff)
        {
            Destroy(GetComponent<Particle>());
        }
    }
}