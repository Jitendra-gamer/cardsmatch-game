using UnityEngine;

namespace CardMatch.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public void SelectedLayout(int layoutIndex)
        {
            // Set GameStats for user selected layout
            GameStats.layoutType = (GameStats.LayoutType)(layoutIndex);
            //Load GamePlay scene
            SceneLoadManager.LoadGamePlay();
        }
    }
}