using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private CardValues _cardValues;

    private Transform _cashedTransform;
    private bool _isMousePressed;
    private Vector3 _position;
    private Quaternion _rotation;
    public CardValues CardValues => _cardValues;

    private void Awake()
    {
        _cashedTransform = transform;
    }

    private void Update()
    {
        if (_isMousePressed)
        {
            Vector3 mousePositionInUnits = Input.mousePosition;
            Vector3 mousePositionInPixels = Camera.main.ScreenToWorldPoint(mousePositionInUnits);
            mousePositionInPixels.z = 0;
            _cashedTransform.position = mousePositionInPixels;
        }
    }

    private void OnMouseDown()
    {
        _position = _cashedTransform.position;
        _rotation = _cashedTransform.rotation;
        _cashedTransform.rotation = Quaternion.identity;
        _isMousePressed = true;
    }

    private void OnMouseUp()
    {
        _isMousePressed = false;
        RaycastHit2D[] raycastHits2D = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.back);

        bool isTable = false;
        Table table = null;
        foreach (RaycastHit2D raycast in raycastHits2D)
        {
            Debug.Log($"{raycast.collider.name}");
            if (raycast.collider.CompareTag("Table"))
            {
                isTable = true;
                table = raycast.collider.gameObject.GetComponent<Table>();
                break;
            }
        }
        if (!isTable || !table.IsTableEmpty)
        {
            _cashedTransform.DORotate(_rotation.eulerAngles, 0.5f);
            _cashedTransform.DOMove(_position, 0.5f);
        }
        else
        {
            if (table != null)
            {
                _cashedTransform.DOMove(table.CardPoint.position, 0.5f);
                table.SetCard(this);
            }
        }
    }

    public void InitCard(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
        
        _cardValues.RandomInitValues();
    }

    public void ChangeRandomValue()
    {
        _cardValues.SetRandomValue();
    }
}