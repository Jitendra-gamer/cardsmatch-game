using UnityEngine;

namespace CardMatch
{
    public class AudioController : MonoBehaviour
    {
       [SerializeField]
        private AudioClip flip,
            matchFailedClip,
            matchSucessClip,
            gameOverClip;
        [SerializeField] private AudioSource audioSource;

        private void OnEnable()
        {
            EventManager.AddListener(Events.CardClickAudio, CardClicked);
            EventManager<int>.AddListener(Events.MatchSuccessful, MatchSucees);
            EventManager.AddListener(Events.GameWin, PlayGameWin);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener(Events.CardClickAudio, CardClicked);
            EventManager<int>.RemoveListener(Events.MatchSuccessful, MatchSucees);
            EventManager.RemoveListener(Events.GameWin, PlayGameWin);
        }

        private void CardClicked()
        {
            PlayOneShot(flip);
        }

        private void MatchSucees(int match)
        {
            PlayOneShot(matchSucessClip);
        }

        private void PlayGameWin()
        {
            PlayOneShot(gameOverClip);
        }
        
        /// <summary>
        /// Play one shot 
        /// </summary>
        /// <param name="clip"></param>
        private void PlayOneShot(AudioClip clip)
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