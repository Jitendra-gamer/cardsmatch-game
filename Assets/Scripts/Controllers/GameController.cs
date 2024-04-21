using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace CardMatch
{
    public class GameController : MonoBehaviour
    {
        private readonly List<ICard> flippedCards = new List<ICard>();

        private const int NumberOfCardToMatch = 2;
        private int requiredMatchesToWin;

        private void OnEnable()
        {
            EventManager<Card>.AddListener(Events.CardClicked, CardClicked);
            EventManager.AddListener(Events.RestartGame, RestartGame);
        }
        private void OnDisable()
        {
            EventManager<Card>.RemoveListener(Events.CardClicked, CardClicked);
            EventManager.RemoveListener(Events.RestartGame, RestartGame);
        }
        private void Awake()
        {
            requiredMatchesToWin = GetRequiredMatchToWin();
            CheckForSavedGameProgressData();
            Debug.Log("RequiredMatchToWin:" + requiredMatchesToWin);
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
        /// Get user selected Grid data
        /// Calculate requiredMatchesToWin
        /// </summary>
        private void CheckForSavedGameProgressData()
        {
            if (PlayerPrefs.HasKey(GameSaver.GameDataKey))
            {
                SaveGameData saveGameData = GameUtility.GetSavedGameProgressData();
                if (saveGameData != null)
                {
                    GameStats.TurnsTaken = saveGameData.turnsTaken;
                    GameStats.CurrentMatchCount = saveGameData.matches;
                    EventManager<int>.Dispatch(Events.MatchSuccessful, GameStats.CurrentMatchCount);
                    EventManager<int>.Dispatch(Events.TurnsUpdateUI, GameStats.TurnsTaken);
                }
            }
        }

        private Card lastClickedCard = null;
        /// <summary>
        /// Game Logic on card click
        /// add clicked card to temp list and check both card has same id for matches
        /// </summary>
        /// <param name="card"></param>
        private void CardClicked(ICard card)
        {
            Debug.Log("Current Match Count -1 " + GameStats.CurrentMatchCount);
            if (!card.IsFlipped())
            {
                card.Flip();
                GameStats.TurnsTaken++;
                EventManager<int>.Dispatch(Events.TurnsUpdateUI, GameStats.TurnsTaken);
                if (lastClickedCard != null)
                {
                    if (card.GetCardID() == lastClickedCard.GetCardID())
                    {
                        //match
                        lastClickedCard.Match();
                        card.Match();
                        Debug.Log("Current Match Count -2 " + GameStats.CurrentMatchCount);
                        GameStats.CurrentMatchCount++;
                        EventManager<int>.Dispatch(Events.MatchSuccessful, GameStats.CurrentMatchCount);
                        
                        StartCoroutine(nameof(CheckGameWin));
                    }
                    else
                    {
                        //matchFailed
                        lastClickedCard.MatchFailed();
                        ((Card)card).MatchFailed();
                        EventManager.Dispatch(Events.MatchFailed);
                    }
                    lastClickedCard = null;
                }
                else
                {
                    lastClickedCard = (Card)card;
                }
            }
        }

        private IEnumerator CheckGameWin()
        {
            Debug.Log("maches: " + GameStats.CurrentMatchCount);
            if (requiredMatchesToWin == GameStats.CurrentMatchCount)
            {
                Debug.Log("You Won");
                //Update the UI vai event
                EventManager.Dispatch(Events.GameWin);
                yield return new WaitForSeconds(0.5f);
                RestartGame();
            }
        }

        /// <summary>
        /// Set matches and turns to 0
        /// </summary>
        private void RestartGame()
        {
            GameStats.ResetValues();
        }
    }
}