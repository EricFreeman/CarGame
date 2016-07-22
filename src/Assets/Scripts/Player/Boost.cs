using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Boost : MonoBehaviour
    {
        public GameObject BoostGameObject;

        private float _previousVertical;

        void Update()
        {
            var currentVertical = Input.GetAxisRaw("Vertical");

            if (_previousVertical < .1f && currentVertical > .1f)
            {
                var boost = Instantiate(BoostGameObject);
                boost.transform.position = transform.position;
                boost.transform.rotation = transform.rotation;
                boost.transform.SetParent(transform);
            }

            _previousVertical = currentVertical;
        }
    }
}