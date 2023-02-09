using UnityEngine;

namespace Deck
{
    public class CardSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _cardPrefab;

        public void Spawn(Sprite cardSprite)
        {
            GameObject cardInstance = Instantiate(_cardPrefab, transform.position, Quaternion.identity);
            Card card = cardInstance.GetComponent<Card>();
            card.SetSprite(cardSprite);
        }
    }
}