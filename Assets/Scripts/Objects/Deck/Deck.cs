using System.Collections;
using System.Collections.Generic;
using Configs;
using UnityEngine;

namespace Deck
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] private CardSpawner _cardSpawner;
        [SerializeField] private SpriteContainer _spriteContainer;

        private List<Sprite> _cardSprites = new();

        private void Awake()
        {
            _cardSprites = _spriteContainer.Sprites;
        }

        private void Start()
        {
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            foreach (Sprite cardSprite in _cardSprites)
            {
                _cardSpawner.Spawn(cardSprite);
                yield return new WaitForSeconds(1f);
            }
        }
    }
}