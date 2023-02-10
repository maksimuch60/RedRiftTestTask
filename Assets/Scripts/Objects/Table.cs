using System;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private Transform _cardPoint;
    
    private bool _isTableEmpty = true;

    public event Action<Card> OnTableBusy;
    
    public Transform CardPoint => _cardPoint;
    public bool IsTableEmpty => _isTableEmpty;

    public void SetCard(Card card)
    {
        _isTableEmpty = false;
        OnTableBusy?.Invoke(card);
    }
}