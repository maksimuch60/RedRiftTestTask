using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CardValuesUI : MonoBehaviour
    {
        [SerializeField] private CardValues _cardValues;

        [SerializeField] private TextMeshPro _hpLabel;
        [SerializeField] private TextMeshPro _attackLabel;
        [SerializeField] private TextMeshPro _manaLabel;

        private Coroutine _countingCoroutine;

        private void OnEnable()
        {
            _cardValues.OnAttackChanged += SetAttackValue;
            _cardValues.OnHpChanged += SetHpValue;
            _cardValues.OnManaChanged += SetManaValue;
        }

        private void OnDisable()
        {
            _cardValues.OnAttackChanged -= SetAttackValue;
            _cardValues.OnHpChanged -= SetHpValue;
            _cardValues.OnManaChanged -= SetManaValue;
        }

        private void SetManaValue(int value)
        {
            UpdateText(value, _manaLabel);
        }

        private void SetHpValue(int value)
        {
            UpdateText(value, _hpLabel);
        }

        private void SetAttackValue(int value)
        {
            UpdateText(value, _attackLabel);
        }
        
        private void UpdateText(int newValue, TextMeshPro label)
        {
            StartCoroutine(CountText(newValue, label));
        }
        
        private IEnumerator CountText(int newValue, TextMeshPro label)
        {
            int countFPS = 5;
            float duration = 2f;
            WaitForSeconds wait = new(1f / countFPS);
            Int32.TryParse(label.text,out int lastValue);
            int stepAmount;
            int difference = newValue - lastValue;

            if (difference < 0)
                stepAmount = Mathf.FloorToInt((difference) / (countFPS * duration));
            else
                stepAmount = Mathf.CeilToInt((difference) / (countFPS * duration));


            for (int i = 0; i < Mathf.Abs(difference); i++)
            {
                lastValue += stepAmount;
                if ((lastValue > newValue && difference > 0) || (lastValue < newValue && difference < 0))
                    lastValue = newValue;

                label.SetText(lastValue.ToString());
                yield return wait;
            }
        }
    }
}