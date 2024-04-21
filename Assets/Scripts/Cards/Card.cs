using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CardMatch
{
    public class Card : MonoBehaviour, ICard
    {
        [SerializeField] private Button cardButton;
        [SerializeField] private Image cardImage;
        [SerializeField] private Sprite unFlipedSprite;
       
        private Sprite flipedSprite;
      
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
            cardButton.onClick.AddListener(CardClicked);
            EventManager.AddListener(Events.GameWin, HideCard);
        }

        private void OnDisable()
        {
            cardButton.onClick.RemoveListener(CardClicked);
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
            flipedSprite = cardData.GetCardSprite();
            cardImage.sprite = flipedSprite;
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
                    break;
                case CardState.Flipped:
                    EventManager<Card>.Dispatch(Events.CardClicked, this);
                    break;
                default:
                    if (!cardData.isSavedData)
                    {
                        StartCoroutine(nameof(RotateCard));
                    }
                    else
                    {
                        cardImage.sprite = unFlipedSprite;
                    }
                    break;
            }
        }

        /// <summary>
        /// Flip the card with Animation
        /// Show the actual image on flip
        /// Set cardState as Flipped/UnFlipped 
        /// </summary>
        public void Flip()
        {    
            isFlipped = !isFlipped;
            
            StopCoroutine(nameof(RotateCard));
            StartCoroutine(nameof(RotateCard));

            cardData.cardState = isFlipped ? CardState.Flipped : CardState.UnFlipped;

        }
        
        /// <summary>
        /// When Match the both card, disable the image and
        /// Set cardState as Matched
        /// </summary>
        public void Match()
        {
            cardImage.sprite = flipedSprite;
            cardData.cardState = CardState.Matched;
            StopCoroutine(nameof(MatchRoutine));
            StartCoroutine(nameof(MatchRoutine));

            Debug.Log("Card Match");
        }

        private IEnumerator MatchRoutine()
        {
            yield return new WaitForSeconds(delay);
            cardImage.enabled = false;
        }
        
        /// <summary>
        /// Flip the card with Animation
        /// Show the actual image on flip
        /// Set cardState as Flipped/UnFlipped 
        /// </summary>
        public void MatchFailed()
        {    
            isFlipped = false;
           
            cardImage.sprite = flipedSprite;
            StopCoroutine(nameof(MatchFailedRoutine));
            StartCoroutine(nameof(MatchFailedRoutine));

             cardData.cardState = isFlipped ? CardState.Flipped : CardState.UnFlipped;
        }

        private IEnumerator MatchFailedRoutine()
        {
            yield return new WaitForSeconds(delay);
            cardImage.sprite = unFlipedSprite;
        }

        /// <summary>
        /// When GameOver/Win then hide the card
        /// </summary>
        private void HideCard()
        {
            cardImage.sprite = flipedSprite;
            StopCoroutine(nameof(HideRoutine));
            StartCoroutine(nameof(HideRoutine));
        }

        private IEnumerator HideRoutine()
        {
            yield return new WaitForSeconds(delay);
            gameObject.SetActive(false);
        }

        private IEnumerator RotateCard()
        {
            for(float i= 180f; i >= 0f; i -= 10f)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                if(i == 90f)
                {
                    cardImage.sprite = !isFlipped ? unFlipedSprite : flipedSprite;
                }
                yield return new WaitForSeconds(0.01f);
            }
        }

        /// <summary>
        /// Send the card to GameController for matches logic 
        /// </summary>
        private void CardClicked()
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