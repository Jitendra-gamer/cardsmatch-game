using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CardMatch.UI
{
    public class GamePlayUIController : MonoBehaviour
    {
        [SerializeField] private Button homeButton;
        [SerializeField] private Button saveButton;
        [SerializeField] private GameObject playAgianButton;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text matchesText;
        [SerializeField] private TMP_Text turnText;

        private void Start()
        {
            EventManager<int>.AddListener(Events.ScoreUpdateUI, ScoreUpdate);
            EventManager.AddListener(Events.GameWin, GameOver);

            homeButton.onClick.AddListener(SaveGameProgress);
            saveButton.onClick.AddListener(SaveGameProgress);
            playAgianButton.SetActive(false);
        }

        private void OnDestroy()
        {
            EventManager<int>.RemoveListener(Events.ScoreUpdateUI, ScoreUpdate);
            EventManager.RemoveListener(Events.GameWin, GameOver);

            homeButton.onClick.RemoveListener(SaveGameProgress);
        }

        private void ScoreUpdate(int score)
        {
            turnText.text = score.ToString();
        }

        private void MatchesUpdate(int matches)
        {
            matchesText.text = matches.ToString();
        }

        private void TurnsUpdate(int turns)
        {
            turnText.text = turns.ToString();
        }

        private void GameOver()
        {
            Debug.Log("GameOver");
            playAgianButton.SetActive(true);
        }

        private void RestartGame()
        {
            playAgianButton.SetActive(false);
            MatchesUpdate(0);
            ScoreUpdate(0);
        }

        private void SaveGameProgress()
        {
            //todo
        }

        public void OnPlayAgain()
        {
            Debug.Log("OnPlayAgain");
            RestartGame();
            EventManager.Dispatch(Events.RestartGame);
        }
    }
}