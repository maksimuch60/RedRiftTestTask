using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardValues : MonoBehaviour
{
    private int _hpValue;
    private int _attackValue;
    private int _manaValue;

    public event Action<int> OnHpChanged;
    public event Action<CardValues> OnHpBelowOne;
    public event Action<int> OnAttackChanged;
    public event Action<int> OnManaChanged;

    public void RandomInitValues()
    {
        _hpValue = Random.Range(1, 10);
        _attackValue = Random.Range(1, 10);
        _manaValue = Random.Range(1, 10);
        
        OnHpChanged?.Invoke(_hpValue);
        OnAttackChanged?.Invoke(_attackValue);
        OnManaChanged?.Invoke(_manaValue);
    }

    public void SetRandomValue()
    {
        int randomCardValue = Random.Range(0, 3);
        int randomValue = Random.Range(-2, 10);

        switch (randomCardValue)
        {
            case 0:
                _hpValue = randomValue;
                OnHpChanged?.Invoke(_hpValue);
                break;
            case 1:
                _attackValue = randomValue;
                OnAttackChanged?.Invoke(_attackValue);
                break;
            case 2:
                _manaValue = randomValue;
                OnManaChanged?.Invoke(_manaValue);
                break;
        }

        if (_hpValue < 1)
        {
            OnHpBelowOne?.Invoke(this);
        }
    }
}