using UnityEngine;

namespace CardMatch
{
    public class GameUtility
    {
        public static Vector2 GetGridType()
        {
            if (PlayerPrefs.HasKey(GameSaver.GameDataKey))
            {
                GameStats.layoutType = GetSavedGameProgressData().stats;
                Debug.Log(GameStats.layoutType);
            }

            Vector2 gridLayout = new Vector2(0, 0);
            switch (GameStats.layoutType)
            {
                case GameStats.LayoutType.Grid2x2:
                    gridLayout = new Vector2(2, 2);
                    break;

                case GameStats.LayoutType.Grid2x3:
                    gridLayout = new Vector2(2, 3);
                    break;

                case GameStats.LayoutType.Grid5x6:
                    gridLayout = new Vector2(5, 6);
                    break;
            }

            return gridLayout;
        }

        public static SaveGameData GetSavedGameProgressData()
        {
            string data = PlayerPrefs.GetString(GameSaver.GameDataKey);
            return JsonUtility.FromJson<SaveGameData>(data);
        }

        public static int GetSavedRequiredMatch()
        {
            string data = PlayerPrefs.GetString(GameSaver.GameDataKey);
            SaveGameData saveGameData = JsonUtility.FromJson<SaveGameData>(data);

            return (int)saveGameData.stats;
        }

        public static GameStats.LayoutType GetSavedGridType()
        {
            string data = PlayerPrefs.GetString(GameSaver.GameDataKey);
            SaveGameData saveGameData = JsonUtility.FromJson<SaveGameData>(data);

            return saveGameData.stats;
        }

        public static CardData[] GetSavedCards()
        {
            string data = PlayerPrefs.GetString(GameSaver.GameDataKey);
            SaveGameData saveGameData = JsonUtility.FromJson<SaveGameData>(data);

            return saveGameData.cardDatas;
        }
    }
}
