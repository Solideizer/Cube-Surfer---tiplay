using UnityEngine;

namespace Managers
{
    [RequireComponent (typeof (AudioSource))]
    public class AudioManager : MonoBehaviour
    {

        #region Variable Declarations
        private static AudioClip _collectSound;
        private static AudioClip _obstacleSound;
        private static AudioClip _gemCollectSound;
        private static AudioSource _audioSource;
        #endregion
        private void Start ()
        {
            LoadAudioFiles ();
            _audioSource = GetComponent<AudioSource> ();
        }

        private static void LoadAudioFiles ()
        {
            _collectSound = Resources.Load<AudioClip> ("collect");
            _obstacleSound = Resources.Load<AudioClip> ("hit");
            _gemCollectSound = Resources.Load<AudioClip> ("gemCollect");
        }

        public static void PlaySound (string clip)
        {
            switch (clip)
            {
                case "collect":
                    _audioSource.PlayOneShot (_collectSound);
                    break;
                case "obstacle":
                    _audioSource.PlayOneShot (_obstacleSound);
                    break;
                case "gem":
                    _audioSource.PlayOneShot (_gemCollectSound);
                    break;

            }
        }
    }
}