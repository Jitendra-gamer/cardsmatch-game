// <summary>
using System.Collections.Generic;
using UnityEngine;

namespace CardMatch
{
    public class ObjectPool : MonoBehaviour
    {
        private Transform cardContainer;
        private int poolSize;
        private GameObject cardPrefab;

        [SerializeField] private List<GameObject> cardPool = new List<GameObject>();

        public void Init(int poolSize, GameObject cardPrefab, Transform cardContainer)
        {
            this.cardContainer = cardContainer;
            this.poolSize = poolSize;
            this.cardPrefab = cardPrefab;
            InitPool();
        }

        /// Populate the object pool with bullets
        /// </summary>
        private void InitPool()
        {
            Debug.Log("InitPool" + poolSize);
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(cardPrefab, cardContainer);
                obj.SetActive(false);
                cardPool.Add(obj);
            }
        }

        /// <summary>
        /// Find and return an inactive card from the pool
        /// If no inactive card is found, create a new one and add it to the pool
        /// </summary>
        /// <returns></returns>
        public Card GetObject()
        {
            // Find and return an inactive card from the pool
            foreach (GameObject card in cardPool)
            {
                if (!card.activeInHierarchy)
                {
                    card.SetActive(true);
                    return card.GetComponent<Card>();
                }
            }

            // If no inactive cards is found, create a new one and add it to the pool
            GameObject newCard = Instantiate(cardPrefab, cardContainer);
            cardPool.Add(newCard);
            return newCard.GetComponent<Card>();
        }
    }
}