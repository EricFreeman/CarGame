using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Camera
{
    public class CameraShake : MonoBehaviour, IListener<ShakeCamera>
    {
        public bool Shaking;
        private float ShakeDecay;
        private float ShakeIntensity;
        private Vector3 OriginalPos;
        private Quaternion OriginalRot;

        void Start()
        {
            Shaking = false;
            OriginalPos = transform.localPosition;
            this.Register<ShakeCamera>();
        }

        void OnDestroy()
        {
            this.UnRegister<ShakeCamera>();
        }

        void Update()
        {
            if (ShakeIntensity >= 0)
            {
                transform.localPosition = OriginalPos + Random.insideUnitSphere * ShakeIntensity;
                transform.localRotation = new Quaternion(OriginalRot.x + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f,
                    OriginalRot.y + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f,
                    OriginalRot.z + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f,
                    OriginalRot.w + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f);

                ShakeIntensity -= ShakeDecay;
            }
            else if (Shaking)
            {
                Shaking = false;
                transform.localPosition = OriginalPos;
                transform.localRotation = OriginalRot;
            }
        }

        public void Handle(ShakeCamera message)
        {
            if (!Shaking)
            {
                OriginalPos = transform.localPosition;
                OriginalRot = transform.localRotation;
            }

            ShakeIntensity = 0.3f;
            ShakeDecay = 0.02f;
            Shaking = true;
        }
    }
}