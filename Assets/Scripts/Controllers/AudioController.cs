using UnityEngine;

namespace CardMatch
{
    /// <summary>
    /// This AudioController is singleton,
    /// We can also achive this by Observer pattern
    /// </summary>
    public class AudioController : Singleton<AudioController>
    {
        [SerializeField] private AudioSource audioSource;

        /// <summary>
        /// Play one shot 
        /// </summary>
        /// <param name="clip"></param>
        public void PlayOneShot(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }

        /// <summary>
        /// Set Mute and unMute as per val
        /// </summary>
        /// <param name="val"></param>
        public void ToggleMute(bool val)
        {
            audioSource.mute = true;
        }
    }
}