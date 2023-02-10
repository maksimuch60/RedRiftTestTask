using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Deck
{
    public class CardSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _cardPrefab;

        private int _sortingGroupCounter;

        public event Action<GameObject> OnSpawned;

        public void Spawn(Sprite cardSprite)
        {
            GameObject cardInstance = Instantiate(_cardPrefab, transform.position, Quaternion.identity);
            
            cardInstance.GetComponent<SortingGroup>().sortingOrder = _sortingGroupCounter++;
            
            Card card = cardInstance.GetComponent<Card>();
            card.InitCard(cardSprite);
            
            OnSpawned?.Invoke(cardInstance);
        }
    }
}