using UnityEngine;

namespace CardMatch
{
    public class GameUtility
    {
        public static Vector2 GetGridType()
        {
            if (PlayerPrefs.HasKey(GameSaver.GameDataKey))
            {
                GameStats.LayoutType = GetSavedGameProgressData().stats;
                Debug.Log(GameStats.LayoutType);
            }

            Vector2 gridLayout = new Vector2(0, 0);
            switch (GameStats.LayoutType)
            {
                case LayoutType.Grid2x2:
                    gridLayout = new Vector2(2, 2);
                    break;

                case LayoutType.Grid2x3:
                    gridLayout = new Vector2(2, 3);
                    break;

                case LayoutType.Grid5x6:
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

        public static void SaveGame()
        {
            GameSaver gameSaver = new GameSaver();
            gameSaver.SaveCardData(GameStats.Cards, GameStats.Score, GameStats.CurrentMatchCount, GameStats.TurnsTaken);
        }
    }
}
