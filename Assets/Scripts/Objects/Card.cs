using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
}