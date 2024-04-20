using UnityEngine;
using UnityEngine.UI;

namespace CardMatch.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        private void Awake()
        {
            if (PlayerPrefs.HasKey(GameSaver.GameDataKey))
            {
                SceneLoadManager.LoadGamePlay();
            }
        }

        public void SelectedLayout(int layoutIndex)
        {
            // Set GameStats for user selected layout
            GameStats.LayoutType = (LayoutType)(layoutIndex);
            //Load GamePlay scene
            SceneLoadManager.LoadGamePlay();
        }
    }
}