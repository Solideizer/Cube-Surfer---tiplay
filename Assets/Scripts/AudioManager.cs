using UnityEngine;

namespace Managers
{
    [RequireComponent (typeof (AudioSource))]
    public class AudioManager : MonoBehaviour
    {

        #region Variable Declarations

        private static AudioClip _collectSound;
        private static AudioClip _obstacleSound;
        private static AudioSource _audioSource;

        #endregion
        private void Start ()
        {
            _collectSound = Resources.Load<AudioClip> ("collect");
            _obstacleSound = Resources.Load<AudioClip> ("hit");

            _audioSource = GetComponent<AudioSource> ();
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

            }
        }
    }
}