using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CardMatch.UI
{
    public class GamePlayUIController : MonoBehaviour
    {
        [SerializeField] private Button homeButton, saveButton;
        [SerializeField] private GameObject playAgianButton;
        [SerializeField] private TMP_Text scoreText,matchesText, turnText;

        private void Awake()
        {
            EventManager<int>.AddListener(Events.ScoreUpdateUI, ScoreUpdate);
            EventManager<int>.AddListener(Events.MatchSuccessfull, MatchesUpdate);
            EventManager<int>.AddListener(Events.TurnsUpdateUI, TurnsUpdate);
            EventManager.AddListener(Events.GameWin, GameOver);

            homeButton.onClick.AddListener(HomeScene);
            saveButton.onClick.AddListener(SaveGameProgress);
            playAgianButton.SetActive(false);
            playAgianButton.GetComponent<Button>().onClick.AddListener(OnPlayAgain);
        }

        private void OnDestroy()
        {
            EventManager<int>.RemoveListener(Events.ScoreUpdateUI, ScoreUpdate);
            EventManager<int>.RemoveListener(Events.MatchSuccessfull, MatchesUpdate);
            EventManager<int>.RemoveListener(Events.TurnsUpdateUI, TurnsUpdate);
            EventManager.RemoveListener(Events.GameWin, GameOver);

            homeButton.onClick.RemoveListener(HomeScene);
            saveButton.onClick.RemoveListener(SaveGameProgress);
            playAgianButton.GetComponent<Button>().onClick.RemoveListener(OnPlayAgain);
        }

        private void ScoreUpdate(int score)
        {
            Debug.Log("Score: " + score);
            scoreText.text = score.ToString();
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
            saveButton.gameObject.SetActive(false);
            playAgianButton.SetActive(true);
        }

        private void HomeScene()
        {
            PlayerPrefs.DeleteKey(GameSaver.GameDataKey);
            PlayerPrefs.Save();
            SceneLoadManager.LoadMainMenu();
        }

        private void SaveGameProgress()
        {
            EventManager.Dispatch(Events.SaveGameProgress);
        }

        public void OnPlayAgain()
        {
            Debug.Log("OnPlayAgain");
            RestartGame();
            EventManager.Dispatch(Events.RestartGame);
        }

        private void RestartGame()
        {
            saveButton.gameObject.SetActive(true);
            playAgianButton.SetActive(false);
            MatchesUpdate(0);
            ScoreUpdate(0);
            TurnsUpdate(0);
        }
    }
}