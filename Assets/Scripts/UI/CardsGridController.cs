using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


namespace CardMatch.UI
{
    [RequireComponent(typeof(ObjectPool))]
    public class CardsGridController : MonoBehaviour
    {
        [SerializeField] private ObjectPool objectPool;

        [SerializeField] private CardType[] cardTypes;              // apple, bannanna, etc // All cards references
        [SerializeField] private GameObject cardPrefab;             // Prefab for the card object
        [SerializeField] private Transform cardContainer;           // Parent transform for card objects
        [SerializeField] private GridLayoutGroup gridLayoutGroup;

        [SerializeField] private Card[] cards;

        private Vector2 layoutSize;
        private int gridSizeX;
        private int gridSizeY;

        private void Awake()
        {
            Initialize();
            EventManager.AddListener(Events.RestartGame, RestartGame);
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener(Events.RestartGame, RestartGame);
        }

        private void RestartGame()
        {
            GenerateCards();
        }

        /// <summary>
        /// Create a grid of given data
        /// shuffle the cards and generate to scene
        /// </summary>
        /// <param name="layoutSize"></param>
        private void Initialize()
        {
            layoutSize = GameUtility.GetGridType();
            gridSizeX = (int)layoutSize.x;
            gridSizeY = (int)layoutSize.y;

            int poolSize = gridSizeX * gridSizeY;
            objectPool.Init(poolSize, cardPrefab, cardContainer);
            GenerateCards();
        }

        private void GenerateCards()
        {
            List<CardType> cardsPair = GenerateCardsPair((gridSizeX * gridSizeY) / 2);

            ArrangeCardsInGrid(cardsPair);
        }

        private List<CardType> GenerateCardsPair(int noOfCards)
        {
            List<CardType> cardsPair = new List<CardType>();

            // load game from saved data if saved
            if (PlayerPrefs.HasKey(GameSaver.GameDataKey))
            {
                CardData[] cardDatas = GameUtility.GetSavedCards();

                for (int i = 0; i < cardDatas.Length; i++)
                {
                    Debug.Log(cardDatas[i].cardId);
                    CardType cardType = cardTypes[cardDatas[i].cardId];

                    cardType.cardState = cardDatas[i].cardState;

                    Debug.Log(cardType.cardId);
                    cardsPair.Add(cardType);
                }

                return cardsPair;
            }

            for (int i = 0; i < noOfCards; i++)
            {
                int randomCardIndex = Random.Range(0, cardTypes.Length);

                CardType cardType = cardTypes[randomCardIndex]; // get randam card from CardTypes array (All cards)

                cardType.cardState = CardState.UnFlipped;
                cardsPair.Add(cardType);
                cardsPair.Add(cardType);
            }

            cardsPair.ShuffleCards();

            return cardsPair;
        }

        private void ArrangeCardsInGrid(List<CardType> cardsPair)
        {
            gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayoutGroup.constraintCount = (int)layoutSize.x;

            float containerWidth = cardContainer.GetComponent<RectTransform>().rect.width;
            float containerHeight = cardContainer.GetComponent<RectTransform>().rect.height;

            float cellSizeInX = containerWidth / layoutSize.x - gridLayoutGroup.spacing.x;
            float cellSizeInY = containerHeight / layoutSize.y - gridLayoutGroup.spacing.y;

            //Set grid cell size
            gridLayoutGroup.cellSize = new Vector2(cellSizeInX, cellSizeInY);
            cards = new Card[cardsPair.Count];
            Debug.Log("--------------------------");
            // To spawn cards, replace your original code with this:
            for (int i = 0; i < cardsPair.Count; i++)
            {
                Card card = objectPool.GetObject();
                card.Initialize(cardsPair[i]);
                Debug.Log(card.cardState);
                cards[i] = card;
            }

            GameObject.FindObjectOfType<GameController>().SetCards(cards);
        }
    }
}