using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "SpriteContainer", menuName = "Configs/SpriteContainer")]
    public class SpriteContainer : ScriptableObject
    {
        public List<Sprite> Sprites = new();

        public void Add(Sprite sprite)
        {
            Sprites.Add(sprite);
        }

        public void Clear()
        {
            Sprites.Clear();
        }
    }
}