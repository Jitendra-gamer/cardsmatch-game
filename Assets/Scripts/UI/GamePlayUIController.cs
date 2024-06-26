using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CardMatch.UI
{
    public class GamePlayUIController : MonoBehaviour
    {
        [SerializeField] private Button homeButton, saveButton;
        [SerializeField] private GameObject playAgianButton;
        [SerializeField] private PopUpController popUpController;
        [SerializeField] private TMP_Text scoreText, matchesText, turnText;

        private void OnEnable()
        {
            EventManager<int>.AddListener(Events.ScoreUpdateUI, ScoreUpdate);
            EventManager<int>.AddListener(Events.MatchSuccessful, MatchesUpdate);
            EventManager<int>.AddListener(Events.TurnsUpdateUI, TurnsUpdate);
            EventManager.AddListener(Events.GameWin, GameOver);

            homeButton.onClick.AddListener(HomeScene);
            saveButton.onClick.AddListener(SaveGameProgress);
            playAgianButton.SetActive(false);
            playAgianButton.GetComponent<Button>().onClick.AddListener(OnPlayAgain);
        }

        private void OnDisable()
        {
            EventManager<int>.RemoveListener(Events.ScoreUpdateUI, ScoreUpdate);
            EventManager<int>.RemoveListener(Events.MatchSuccessful, MatchesUpdate);
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
            popUpController.ShowPopUp("You Won!! Play Again or Try another level");
            saveButton.gameObject.SetActive(false);
            playAgianButton.SetActive(true);
        }

        private void HomeScene()
        {
            GameStats.ResetValues();
            SceneLoadManager.LoadMainMenu();
        }

        private void SaveGameProgress()
        {
            popUpController.ShowPopUp("Game progress saved! Feel free to exit now. When you return, you'll pick up right where you left off, unless you clich home button or finish the game.");
            GameUtility.SaveGame();
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
            popUpController.HidePopUp();
            MatchesUpdate(0);
            ScoreUpdate(0);
            TurnsUpdate(0);
        }
    }
}