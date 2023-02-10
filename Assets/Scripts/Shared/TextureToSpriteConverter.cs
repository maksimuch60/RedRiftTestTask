using System.Collections.Generic;
using UnityEngine;

namespace Shared
{
    public static class TextureToSpriteConverter
    {
        public static Sprite Convert(Texture2D texture)
        {
            Sprite sprite = Sprite.Create(texture,
                new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return sprite;
        }
    }
}