using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraTrack : MonoBehaviour
    {
        public GameObject GameObject;
        public Vector3 Offset;

        void Start()
        {
            transform.position = GameObject.transform.position + Offset;
        }

        void Update()
        {
            transform.position = GameObject.transform.position + Offset;
        }
    }
}