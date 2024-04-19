using System;

namespace CardMatch
{
    [Serializable]
    public class SaveGameData
    {
        public GameStats.LayoutType stats;
        public CardData[] cardDatas;
        public int scores;
        public int turnsTaken;
        public int scoreComboMultiplier;

        public int matches;
    }
}
