using Assets.Scripts.Extensions;
using Assets.Scripts.Messages;
using UnityEngine;
using UnityEventAggregator;

namespace Assets.Scripts.Camera
{
    public class CameraKickBack : MonoBehaviour, IListener<KickBackCamera>
    {
        public float CorrectionSpeed = 1;
        public float Cuttoff = .01f;
        private bool _isKicking;

        void Start()
        {
            this.Register<KickBackCamera>();
        }

        void OnDestroy()
        {
            this.UnRegister<KickBackCamera>();
        }

        void Update()
        {
            if (_isKicking)
            {
                transform.localPosition = transform.localPosition.MoveTowards(Vector3.zero, CorrectionSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.localPosition, Vector3.zero) < Cuttoff)
                {
                    transform.localPosition = Vector3.zero;
                    _isKicking = false;
                }
            }
        }

        public void Handle(KickBackCamera message)
        {
            if (!_isKicking)
            {
                var adjusted = new Vector3(message.Direction.x, message.Direction.z, 0);
                transform.Translate(adjusted * message.KickAmount);
                _isKicking = true;
            }
        }
    }
}