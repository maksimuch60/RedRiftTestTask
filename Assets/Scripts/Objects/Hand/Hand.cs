using System.Collections.Generic;
using Deck;
using DG.Tweening;
using UnityEngine;

namespace Hand
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private CardSpawner _cardSpawner;
        [SerializeField] private Table _table;
        [SerializeField] private Transform _arcCenterPoint;
        [SerializeField] private float _radius;
        
        public List<GameObject> _cardList = new();
        private readonly List<CustomTransform> _cardTransformList = new();

        private int _cardPointsAmount;

        private Vector3 _arcCenterPointPosition;

        public List<GameObject> CardList => _cardList;

        private void Awake()
        {
            _arcCenterPointPosition = _arcCenterPoint.position;
        }

        private void OnEnable()
        {
            _cardSpawner.OnSpawned += GoToHand;
            _table.OnTableBusy += RemoveCard;
        }

        private void OnDisable()
        {
            _cardSpawner.OnSpawned -= GoToHand;
            _table.OnTableBusy -= RemoveCard;
            foreach (GameObject card in _cardList)
            {
                if (card != null)
                {
                    card.GetComponent<Card>().CardValues.OnHpBelowOne -= DestroyCard;
                }
            }
        }

        private void RemoveCard(Card card)
        {
            _cardList.Remove(card.gameObject);
            ChangeHand();
        }

        private void DestroyCard(CardValues cardValues)
        {
            GameObject card = cardValues.gameObject;
            _cardList.Remove(card);
            card.GetComponent<Card>().CardValues.OnHpBelowOne -= DestroyCard;
            ChangeHand();
            card.transform.DORotate(new Vector3(0.0f, 0.0f, 0.0f), 0.5f);
            Tween tween = DOTween.Sequence();
            tween = card.transform.
                DOMove(new Vector3(0.0f, card.transform.position.y + 5f), 2f).
                OnComplete(()=>Destroy(card));
        }

        private void GoToHand(GameObject card)
        {
            _cardList.Add(card);
            card.GetComponent<Card>().CardValues.OnHpBelowOne += DestroyCard;
            ChangeHand();
        }

        private void ChangeHand()
        {
            CalculateCardPositions();
            MoveCards();
            ResetCustomTransformList();
        }

        private void ResetCustomTransformList()
        {
            _cardTransformList.Clear();
        }

        private void CalculateCardPositions()
        {
            _cardPointsAmount = _cardList.Count;
            float range = GetRange(_cardPointsAmount);
            float deltaAlpha = 0;
            if (_cardPointsAmount > 1)
            {
                deltaAlpha = range / ((float)_cardPointsAmount - 1);
            }
            float angle = range * 0.5f + deltaAlpha;
            
            for (int i = 0; i < _cardPointsAmount; i++)
            {
                float x = _radius * Mathf.Sin((-angle + deltaAlpha) * Mathf.Deg2Rad) + _arcCenterPointPosition.x;
                float y = _radius * Mathf.Cos((-angle + deltaAlpha) * Mathf.Deg2Rad) + _arcCenterPointPosition.y;
                
                Vector3 pointTransformPosition = new (x, y, 0f);
                Vector3 backVector = new(0.0f, 0.0f, -(-angle + deltaAlpha) * Mathf.Deg2Rad * 50f);
                
                CustomTransform customTransform = new CustomTransform(pointTransformPosition, backVector);
                _cardTransformList.Add(customTransform);
                angle -= deltaAlpha;
            }
        }

        private float GetRange(int cardPointsAmount)
        {
            return (cardPointsAmount - 1) * 7.5f;
        }

        private void MoveCards()
        {
            for(int i = 0; i < _cardList.Count; i++)
            {
                _cardList[i].transform.DOMove(_cardTransformList[i].Position, 0.75f);
                _cardList[i].transform.DORotate(_cardTransformList[i].Back, 0.5f);
            }
        }
    }
}