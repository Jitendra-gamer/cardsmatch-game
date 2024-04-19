using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace CardMatch
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private AudioClip matchFailedClip,
            matchSucessClip,
            gameOverClip;

        private readonly List<ICard> flippedCards = new List<ICard>();

        private const int NumberOfCardToMatch = 2;
        private int currentMatchCount = 0;
        private int requiredMatchesToWin;

        private void Start()
        {
            requiredMatchesToWin = GetRequiredMatchToWin();
            EventManager<Card>.AddListener(Events.CardClicked, CardClicked);
            EventManager.AddListener(Events.RestartGame, RestartGame);
        }

        private void OnDestroy()
        {
            EventManager<Card>.RemoveListener(Events.CardClicked, CardClicked);
            EventManager.RemoveListener(Events.RestartGame, RestartGame);
        }

        /// <summary>
        /// Get user selected Grid data
        /// Calculate requiredMatchesToWin
        /// </summary>
        private int GetRequiredMatchToWin()
        {
            Vector2 gridLayout = GameUtility.GetGridType();
            return (int)((gridLayout.x * gridLayout.y) * 0.5f);
        }

        /// <summary>
        /// Game Logic on card click
        /// add clicked card to temp list and check both card has same id for matches
        /// </summary>
        /// <param name="card"></param>
        private void CardClicked(ICard card)
        {
            if (!card.IsFlipped() && flippedCards.Count < NumberOfCardToMatch)
            {
                card.Flip();
                flippedCards.Add(card);

                if (flippedCards.Count == NumberOfCardToMatch) //NumberOfCardToMatch =2
                {
                    CheckForMatch();
                }
            }
        }

        /// <summary>
        /// Check the id of all flippedCards list
        /// if ids are same then match 
        /// </summary>
        private void CheckForMatch()
        {
            bool allCardsHaveSameId = flippedCards.All(card => card.GetCardID() == flippedCards[0].GetCardID());

            for (int i = 0; i < flippedCards.Count; i++)
            {
                if (allCardsHaveSameId)
                {
                    // Cards match
                    flippedCards[i].Match();
                }
                else
                {
                    // Cards don't match
                    flippedCards[i].Flip();
                }
            }

            flippedCards.Clear();

            if (allCardsHaveSameId)
            {
                currentMatchCount++;
                EventManager.Dispatch(Events.MatchSuccessfull);
                AudioController.GetInstance().PlayOneShot(matchSucessClip);

                CheckGameWin();
            }
            else
            {
                EventManager.Dispatch(Events.MatchFailed);
                AudioController.GetInstance().PlayOneShot(matchFailedClip);
            }
        }


        private void CheckGameWin()
        {
            Debug.Log("maches: " + currentMatchCount);
            if (requiredMatchesToWin == currentMatchCount)
            {
                // Audio for game over
                Debug.Log("You Won");
                AudioController.GetInstance().PlayOneShot(gameOverClip);

                //Update the UI vai event
                EventManager.Dispatch(Events.GameWin);
            }
        }

        /// <summary>
        /// Set matches and turns to 0
        /// </summary>
        private void RestartGame()
        {
            currentMatchCount = 0;
        }
    }
}