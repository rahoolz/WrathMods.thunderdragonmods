using Kingmaker.Blueprints.JsonSystem.Converters;
using Kingmaker.Blueprints;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using static thunderdragonmods.Archetypes.SacredFistTT;

namespace thunderdragonmods.Utils
{
    internal class ImportSprite
    {
        internal static Sprite CreateSprite(string image)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(image);
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            var texture = new Texture2D(64, 64, TextureFormat.RGBA32, false);
            _ = texture.LoadImage(bytes);
            texture.name = image + ".texture";
            var sprite = Sprite.Create(texture, new(0, 0, texture.width, texture.height), Vector2.zero, 100);
            sprite.name = image + ".sprite";
            return sprite;
        }
    }
}
