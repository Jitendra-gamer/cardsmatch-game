using CardMatch;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Pair")]
public class CardType : ScriptableObject
{
    public int cardId;
    public string cardName;
    public CardState cardState = CardState.UnFlipped; //Intial state of card
    public Sprite cardImage; // You can use this for the card's image/icon
}