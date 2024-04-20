using System;
using UnityEngine;

namespace CardMatch
{
    public class ScoreController : MonoBehaviour
    {
        private float comboMultiplier = 1.0f;
        private const int ScoreValue = 10;

        private void Awake()
        {
            EventManager<int>.AddListener(Events.MatchSuccessful, ScoreUpdate);
            EventManager.AddListener(Events.MatchFailed, MatchFailed);
            EventManager.AddListener(Events.RestartGame, RestartGame);
        }

        private void OnDestroy()
        {
            EventManager<int>.RemoveListener(Events.MatchSuccessful, ScoreUpdate);
            EventManager.RemoveListener(Events.MatchFailed, MatchFailed);
            EventManager.RemoveListener(Events.RestartGame, RestartGame);
        }

        private void ScoreUpdate(int updatedScore)
        {
            GameStats.Score = Mathf.FloorToInt(updatedScore * ScoreValue * comboMultiplier);
            //I am giving bonus for combo Matches 
            comboMultiplier += 0.5f;
            //Update UI 
            EventManager<int>.Dispatch(Events.ScoreUpdateUI, GameStats.Score);
        }

        private void MatchFailed()
        {
            comboMultiplier = 1.0f;
            //if we want we can give panality by deducting score by some amount
        }

        private void RestartGame()
        {
            comboMultiplier = 1.0f;
        }
    }
}
