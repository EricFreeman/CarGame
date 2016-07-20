using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Camera
{
    public class CameraShake : MonoBehaviour, IListener<ShakeCamera>
    {
        public bool Shaking;
        private float _shakeDecay;
        private float _shakeIntensity;
        private Vector3 _originalPos;
        private Quaternion _originalRot;

        void Start()
        {
            Shaking = false;
            _originalPos = transform.localPosition;
            this.Register<ShakeCamera>();
        }

        void OnDestroy()
        {
            this.UnRegister<ShakeCamera>();
        }

        void Update()
        {
            if (_shakeIntensity >= 0)
            {
                transform.localPosition = _originalPos + Random.insideUnitSphere * _shakeIntensity;
                transform.localRotation = new Quaternion(_originalRot.x + Random.Range(-_shakeIntensity, _shakeIntensity) * .2f,
                    _originalRot.y + Random.Range(-_shakeIntensity, _shakeIntensity) * .2f,
                    _originalRot.z + Random.Range(-_shakeIntensity, _shakeIntensity) * .2f,
                    _originalRot.w + Random.Range(-_shakeIntensity, _shakeIntensity) * .2f);

                _shakeIntensity -= _shakeDecay;
            }
            else if (Shaking)
            {
                Shaking = false;
                transform.localPosition = _originalPos;
                transform.localRotation = _originalRot;
            }
        }

        public void Handle(ShakeCamera message)
        {
            if (!Shaking)
            {
                _originalPos = transform.localPosition;
                _originalRot = transform.localRotation;
            }

            _shakeIntensity = message.ShakeIntensity;
            _shakeDecay = message.ShakeDecay;
            Shaking = true;
        }
    }
}