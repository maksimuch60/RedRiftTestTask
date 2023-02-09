using System;
using System.Collections.Generic;
using System.Linq;
using Shared;
using UnityEngine;
using WebModule;
using Random = UnityEngine.Random;

namespace Deck
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] private CardSpawner _cardSpawner;
        [SerializeField] private GetImageWebModule _getImageWebModule;
        [SerializeField] private int _minDeckSize = 4;
        [SerializeField] private int _maxDeckSize = 7;
        
        private int _deckSize;
        private List<Sprite> _cardSprites = new();

        private void Awake()
        {
            _deckSize = Random.Range(_minDeckSize, _maxDeckSize);
        }

        private void Start()
        {
            _getImageWebModule.LoadData(_deckSize);
            _cardSprites = TextureToSpriteConverter.Convert(_getImageWebModule.Textures);

            Spawn();
        }

        private void Spawn()
        {
            _cardSpawner.Spawn(_cardSprites.First());
        }
    }
}