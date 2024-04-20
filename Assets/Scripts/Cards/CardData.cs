using UnityEngine;
using System;

namespace CardMatch
{
    [Serializable]
    public class CardData
    {
        public int cardId;
        public string cardName;
        private Sprite cardImage; // You can use this for the card's image/icon
        public CardState cardState;
        public bool isSavedData = false;
        public CardData(CardTypeSO cardTypeSO){
            cardId = cardTypeSO.cardId;
            cardName = cardTypeSO.cardName;
            cardImage = cardTypeSO.cardImage;
            cardState = CardState.UnFlipped;
        }
        public CardData(int cardId,string cardName,Sprite cardImage )
        {
            this.cardId = cardId;
            this.cardName = cardName;
            this.cardImage = cardImage;
            cardState = CardState.UnFlipped;
        }
        public Sprite GetCardSprite()
        {
            return cardImage;
        }
    }
}