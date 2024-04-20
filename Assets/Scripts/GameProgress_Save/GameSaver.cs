using UnityEngine;

namespace CardMatch
{
    public class GameSaver
    {
        public const string GameDataKey = "GameData";
        private SaveGameData data = new SaveGameData();

        public void SaveCardData(Card[] cards, int score, int currentMatches, int turnsTaken)
        {
            data.cardDatas = new CardData[cards.Length];
            for (int i = 0; i < cards.Length; i++)
            {
                data.cardDatas[i] =cards[i].GetCardData();
                data.cardDatas[i].isSavedData = true;
            }

            data.matches = currentMatches;
            data.turnsTaken = turnsTaken;
            data.scores = score;
            data.stats = GameStats.LayoutType;
            PlayerPrefs.SetString(GameDataKey, JsonUtility.ToJson(data));
            PlayerPrefs.Save();
            Debug.Log(PlayerPrefs.GetString(GameDataKey));
        }
    }
}