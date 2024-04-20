using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


namespace CardMatch.UI
{
    [RequireComponent(typeof(ObjectPool))]
    public class CardsGridController : MonoBehaviour
    {
        [SerializeField] private ObjectPool objectPool;
        [SerializeField] private CardTypeSO[] cardTypes;            // apple, bannanna, etc // All cards references
        [SerializeField] private GameObject cardPrefab;             // Prefab for the card object
        [SerializeField] private Transform cardContainer;           // Parent transform for card objects
        [SerializeField] private GridLayoutGroup gridLayoutGroup;

        private Vector2 layoutSize;
        private int gridSizeX;
        private int gridSizeY;
        private Dictionary<int, CardTypeSO> cardTypeIdMap = new Dictionary<int, CardTypeSO>();

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

            IntializeCardMap();
            GenerateCards();
        }

        private void IntializeCardMap()
        {
            for (int i = 0; i < cardTypes.Length; i++)
            {
                if (!cardTypeIdMap.ContainsKey(cardTypes[i].cardId))
                {
                    cardTypeIdMap.Add(cardTypes[i].cardId, cardTypes[i]);
                }
            }
        }

        private void GenerateCards()
        {
            List<CardData> cardsPair = GetCardPairData((gridSizeX * gridSizeY) / 2);
            SetGridLayout();
            ArrangeCardsInGrid(cardsPair);
        }

        private List<CardData> GetCardPairData(int noOfCards)
        {
            List<CardData> cardsPair = new List<CardData>();

            bool isSaveDataLoaded;
            GetCardsFromSavedData(cardsPair, out isSaveDataLoaded);

            if (!isSaveDataLoaded)
            {
                CreateNewCardPairs(noOfCards, cardsPair);
            }

            return cardsPair;
        }

        private void CreateNewCardPairs(int noOfCards, List<CardData> cardsPair)
        {
            for (int i = 0; i < noOfCards; i++)
            {
                int randomCardIndex = Random.Range(0, cardTypes.Length);

                CardTypeSO cardType = cardTypes[randomCardIndex]; // get randam card from CardTypes array (All cards)

                //As there are two cards in pair so we need to add twice 
                cardsPair.Add(new CardData(cardType));
                cardsPair.Add(new CardData(cardType));
            }

            cardsPair.ShuffleCards();
        }

        private void GetCardsFromSavedData(List<CardData> cardsPair, out bool isSaveDataLoaded)
        {
            isSaveDataLoaded = false;

            // load game from saved data if saved
            if (PlayerPrefs.HasKey(GameSaver.GameDataKey))
            {
                SaveGameData savedGameData = GameUtility.GetSavedGameProgressData();
                // Ensure savedGameData and cardDatas are not null
                if (savedGameData != null && savedGameData.cardDatas != null)
                {
                    isSaveDataLoaded = true;
                    foreach (CardData cardData in savedGameData.cardDatas)
                    {
                        CardTypeSO cardType = cardTypeIdMap[cardData.cardId];
                        CardData pairData = new CardData(cardType)
                        {
                            cardState = cardData.cardState,
                            isSavedData = cardData.isSavedData
                        };

                        cardsPair.Add(pairData);
                    }
                }
            }
        }

        private void SetGridLayout()
        {
            gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayoutGroup.constraintCount = (int)layoutSize.x;

            float containerWidth = cardContainer.GetComponent<RectTransform>().rect.width;
            float containerHeight = cardContainer.GetComponent<RectTransform>().rect.height;
            
            float cellSizeInX = (containerWidth / layoutSize.x) - gridLayoutGroup.spacing.x;
            float cellSizeInY = (containerHeight / layoutSize.y) - gridLayoutGroup.spacing.y;
            
            float minSize = Mathf.Min(cellSizeInX, cellSizeInY);
            //Set grid cell size
            gridLayoutGroup.cellSize = new Vector2(minSize, minSize);
        }

        private void ArrangeCardsInGrid(List<CardData> cardsPair)
        {
            Card[] cards = new Card[cardsPair.Count];
            
            for (int i = 0; i < cardsPair.Count; i++)
            {
                Card card = objectPool.GetObject();
               
                card.Initialize(cardsPair[i]);
                cards[i] = card;
            }
            
            GameStats.SetCards(cards);
        }
    }
}