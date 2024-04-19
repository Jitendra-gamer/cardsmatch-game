using UnityEngine;
using System;

namespace CardMatch
{
    [Serializable]
    public class CardData
    {
        public CardState cardState;
        public int cardId;
    }

    public class GameSaver
    {
        public const string GameDataKey = "GameData";
        private SaveGameData data = new SaveGameData();

        public void SaveCardData(Card[] cards, int score, int currentMatches, int turnsTaken)
        {
            data.cardDatas = new CardData[cards.Length];
            for (int i = 0; i < cards.Length; i++)
            {
                data.cardDatas[i] = new CardData();
                data.cardDatas[i].cardId = cards[i].GetCardID();
                data.cardDatas[i].cardState = cards[i].cardState;
            }

            data.matches = currentMatches;
            data.turnsTaken = turnsTaken;
            data.scores = score;
            data.stats = GameStats.layoutType;

            PlayerPrefs.SetString(GameDataKey, JsonUtility.ToJson(data));
            PlayerPrefs.Save();
            Debug.Log(PlayerPrefs.GetString(GameDataKey));
        }
    }
}