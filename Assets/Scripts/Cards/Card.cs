using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace CardMatch
{
    public class Card : MonoBehaviour, ICard
    {
        #region Private Variables
        [SerializeField] private Image cardImage;
        [SerializeField] AudioClip flip;

        private bool isFlipped = false;
        private CardType cardType;
        #endregion

        #region Public Variables
        public bool IsFlipped() => isFlipped;
        public int GetCardID() => cardType.cardId;
        public CardState cardState { get; private set; }
        #endregion

        #region Unity Method
        private void Start()
        {
            EventManager.AddListener(Events.GameWin, HideCard);
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener(Events.GameWin, HideCard);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// When GameOver/Win then hide the card
        /// </summary>
        private void HideCard() => gameObject.SetActive(false);

        #endregion

        #region Public Method
        /// <summary>
        /// Initialize the card required Property
        /// Show Card for few millisecond
        /// </summary>
        /// <param name="pair"></param>
        public void Initialize(CardType pair)
        {
            cardType = pair;
            isFlipped = true;
            cardImage.sprite = pair.cardImage;
            cardImage.enabled = true;
            cardState = pair.cardState;
            Debug.Log("Initialize" + cardState);

            Flip();
        }

        /// <summary>
        /// Flip the card with Animation
        /// Show the actual image on flip
        /// Set cardState as Flipped/UnFlipped 
        /// </summary>
        public async void Flip()
        {
            // Todo Implement visuals or animation for matching cards
            isFlipped = !isFlipped;
            if (isFlipped)
                cardState = CardState.Flipped;
            else
                cardState = CardState.UnFlipped;

            if (!isFlipped)
            {
                await Task.Delay(500);
            }

            cardImage.enabled = isFlipped;
        }

        /// <summary>
        /// When Match the both card, disable the image and
        /// Set cardState as Matched
        /// </summary>
        public async void Match()
        {
            Debug.Log("Card Match");
            cardState = CardState.Matched;
            //
            // Todo Implement visuals or animation for matching cards
            //
            await Task.Delay(500);
            cardImage.enabled = false;
            GetComponent<Image>().enabled = false;
        }

        /// <summary>
        /// Send the card to GameController for matches logic 
        /// </summary>
        public void CardClicked()
        {
            // Todo Implement visuals or animation for click cards
            AudioController.GetInstance().PlayOneShot(flip);
            EventManager<Card>.Dispatch(Events.CardClicked, this);
        }

        #endregion
    }
}