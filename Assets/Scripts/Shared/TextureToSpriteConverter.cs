using System.Collections.Generic;
using UnityEngine;

namespace Shared
{
    public static class TextureToSpriteConverter
    {
        public static List<Sprite> Convert(List<Texture2D> textures)
        {
            List<Sprite> sprites = new();
            foreach (Texture2D texture in textures)
            {
                sprites.Add(
                    Sprite.Create(texture, 
                        new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f)));
            }

            return sprites;
        }
    }
}