using UnityEngine.SceneManagement;

namespace CardMatch
{
    public class SceneLoadManager
    {
        private const string MainMenu = "MainMenu";
        private const string GamePlay = "GamePlay";

        /// <summary>
        /// Load MainMenu Scene as Async
        /// </summary>
        public static void LoadMainMenu()
        {
            SceneManager.LoadSceneAsync(MainMenu);
        }

        /// <summary>
        /// Load GamePlay Scene as Async
        /// </summary>
        public static void LoadGamePlay()
        {
            SceneManager.LoadSceneAsync(GamePlay);
        }
    }
}
