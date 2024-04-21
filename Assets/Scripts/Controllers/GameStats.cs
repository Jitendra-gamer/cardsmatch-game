using System.Diagnostics;
using UnityEngine;

namespace CardMatch
{
    /// <summary>
    /// LayoutType,
    /// We can add any layout here 
    /// </summary>
    public enum LayoutType
    {
        Grid2x2 = 0,
        Grid2x3 = 1,
        Grid5x6 = 2
    }
    public class GameStats
    {
        public static Card[] Cards;
        public static int Score;
        public static void SetCards(Card[] cards)
        {
            Cards = cards;
        }
        public static LayoutType LayoutType { get; set; }

        public static int TurnsTaken = 0;
        public static int CurrentMatchCount = 0;
        public static void ResetValues()
        {
            for (int i = 0; i < Cards.Length; i++)
            {
                Cards[i].ResetCardFlip();
            }
            TurnsTaken = 0;
            CurrentMatchCount = 0;
            Score = 0;
            PlayerPrefs.DeleteKey(GameSaver.GameDataKey);
            PlayerPrefs.Save();
        }
    }
}