using UnityEngine;

[CreateAssetMenu(menuName = "Card Pair")]
public class CardTypeSO : ScriptableObject
{
    public int cardId;
    public string cardName;
    public Sprite cardImage; // You can use this for the card's image/icon
}
