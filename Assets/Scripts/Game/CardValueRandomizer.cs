using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class CardValueRandomizer : MonoBehaviour
    {
        [SerializeField] private Hand.Hand _hand;

        public List<GameObject> _cardList = new();

        private IEnumerator SetCardsValues()
        {
            WaitForSeconds wait = new(0.5f);
            SetCardList();
            while (_hand.CardList.Count > 0)
            {
                SetCardList();
                int cardNumber = 1;
                foreach (GameObject card in _cardList)
                {
                    Card cardComponent = card.GetComponent<Card>();
                    cardComponent.ChangeRandomValue();
                    Debug.Log($"{cardNumber}");
                    cardNumber++;
                    yield return wait;
                }
            }
        }

        private void SetCardList()
        {
            _cardList.Clear();
            foreach (GameObject card in _hand.CardList) 
            {
                if (card != null) 
                {
                    _cardList.Add(card);
                }
            }
        }

        public void Play()
        {
            StartCoroutine(SetCardsValues());
        }
    }
}