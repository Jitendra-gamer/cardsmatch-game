using System;
using System.Collections.Generic;

namespace CardMatch
{
    [Serializable]
    public class SaveGameData
    {
        public LayoutType stats;
        public CardData[] cardDatas;
        public int scores;
        public int turnsTaken;
        public int scoreComboMultiplier;

        public int matches;
    }
}
