using UnityEngine;

namespace Assets.Scripts.General
{
    public class AudioSourceHelper : MonoBehaviour
    {
        private AudioSource _audioSource;
        private float _pitchRandom = .05f;
        private float _volumeRandom = .15f;

        void Start()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        public void Play(AudioClip clip)
        {
            _audioSource.clip = clip;
            _audioSource.PlayOneShot(clip);
            RandomizeAudio();
        }

        public void PlayLoop(AudioClip clip, ulong delay = 0)
        {
            _audioSource.clip = clip;
            _audioSource.loop = true;
            _audioSource.Play(delay);
       
        }

        public void Stop()
        {
            _audioSource.Stop();
        }

        public AudioClip GetAudioClip()
        {
            return _audioSource.clip;
        }

        private void RandomizeAudio()
        {
            _audioSource.pitch = 1 + Random.Range(-_pitchRandom, _pitchRandom);
            _audioSource.volume = 1 - Random.Range(0, _volumeRandom);
        }
    }
}