using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.PlayButton
{
    public class PlayButton : MonoBehaviour
    {
        private RectTransform _transform;

        void Start()
        {
            _transform = GetComponent<RectTransform>();
        }

        void Update()
        {
            _transform.localPosition += new Vector3(0, Mathf.Sin(Time.fixedTime * 2) / 4);
        }

        public void OnClick()
        {
            SceneManager.LoadScene("Game");
        }
    }
}