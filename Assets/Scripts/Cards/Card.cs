using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;
using System.Collections;

namespace CardMatch
{
    public class Card : MonoBehaviour, ICard
    {
        [SerializeField] private Image cardImage, unFlipedImage;
        private bool isFlipped = false;
        private CardData cardData;

        private float delay = 0.5f;

        public bool IsFlipped ()
        {
            return isFlipped;
        }
        public int GetCardID() => cardData.cardId;

        private void OnEnable()
        {
            EventManager.AddListener(Events.GameWin, HideCard);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener(Events.GameWin, HideCard);
        }

        /// <summary>
        /// Initialize the card required Property
        /// Show Card for few millisecond
        /// </summary>
        /// <param name="pair"></param>
        public void Initialize(CardData cardData)
        {
            this.cardData = cardData;
            cardImage.sprite = cardData.GetCardSprite();
            cardImage.enabled = true;

            Debug.Log("cardState: " + cardData.cardState);
            SetCardState(cardData);
        }

        private void SetCardState(CardData cardData)
        {
            switch (cardData.cardState)
            {
                case CardState.Matched:
                    cardImage.enabled = false;
                    unFlipedImage.enabled = false;
                    break;
                case CardState.Flipped:
                    cardImage.enabled = true;
                    break;
                default:
                    if (!cardData.isSavedData)
                    {
                        unFlipedImage.enabled = true;
                        ShowPreview();
                    }
                    else
                    {
                        cardImage.enabled = false;
                    }

                    break;
            }
        }

        private void ShowPreview()
        {
            StopCoroutine(nameof(FlipRoutine));
            StartCoroutine(nameof(FlipRoutine));
        }

        IEnumerator FlipRoutine()
        {
            cardImage.enabled = true;
            yield return new WaitForSeconds(delay);
            cardImage.enabled = false;
        }

        /// <summary>
        /// Flip the card with Animation
        /// Show the actual image on flip
        /// Set cardState as Flipped/UnFlipped 
        /// </summary>
        public void Flip()
        {            
            StopCoroutine(nameof(FlipRoutine));
            Debug.Log("flip "+cardData.cardName+" "+name+" "+transform.GetSiblingIndex());
           
            isFlipped = !isFlipped;
            cardImage.enabled = isFlipped;//isFlipped;
            cardData.cardState = isFlipped ? CardState.Flipped : CardState.UnFlipped;
        }
        
        /// <summary>
        /// When Match the both card, disable the image and
        /// Set cardState as Matched
        /// </summary>
        public void Match()
        {
            StopCoroutine(nameof(MatchRoutine));
            StartCoroutine(nameof(MatchRoutine));

            Debug.Log("Card Match");
        }

        private IEnumerator MatchRoutine()
        {
            cardData.cardState = CardState.Matched;
            
            yield return new WaitForSeconds(delay);
            cardImage.enabled = false;
            unFlipedImage.enabled = false;
        }

        /// <summary>
        /// When GameOver/Win then hide the card
        /// </summary>
        private void HideCard()
        {
            StopCoroutine(nameof(HideRoutine));
            StartCoroutine(nameof(HideRoutine));
        }

        private IEnumerator HideRoutine()
        {
            yield return new WaitForSeconds(delay);
            gameObject.SetActive(false);
        }

        public void MatchFailed()
        {
            isFlipped = false;
            ShowPreview();
        }

        /// <summary>
        /// Send the card to GameController for matches logic 
        /// </summary>
        public void CardClicked()
        {
            EventManager.Dispatch(Events.CardClickAudio);
            EventManager<Card>.Dispatch(Events.CardClicked, this);
        }

        public void ResetCardFlip()
        {
            isFlipped = false;
            cardData.isSavedData = false;
        }

        public  CardData GetCardData()
        {
            return cardData;
        }
    }
}