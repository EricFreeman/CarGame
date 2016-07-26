using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.MainMenu
{
    public class PlayButton : MonoBehaviour
    {
        public AudioClip Music;

        private AudioSource _audioSource;
        private AudioLowPassFilter _lowPass;
        private RectTransform _transform;

        void Start()
        {
            _transform = GetComponent<RectTransform>();
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.clip = Music;
            _audioSource.loop = true;
            _audioSource.Play();

            _lowPass = gameObject.AddComponent<AudioLowPassFilter>();
        }

        void Update()
        {
            _transform.localPosition += new Vector3(0, Mathf.Sin(Time.fixedTime * 2) / 4);

            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
            else if (Input.anyKey)
            {
                OnClick();
            }

            UpdateAudio();
        }

        private void UpdateAudio()
        {
            var distance = Vector3.Distance(_transform.position, Input.mousePosition);

            var desiredFrequency = (int) (6000 - distance*8 - distance*3 - distance*3 - distance*3 - distance*3);

            _lowPass.cutoffFrequency = Mathf.Max(desiredFrequency, 200);
        }

        public void OnClick()
        {
            SceneManager.LoadScene("Game");
        }
    }
}