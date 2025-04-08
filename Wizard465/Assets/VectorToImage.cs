using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets
{
    public class VectorToImage
    {
        public static Texture2D Vector2ListToTexture(List<Vector2> points, int width, int height, Color pointColor, Color backgroundColor)
        {
            Texture2D texture = new Texture2D(width, height);
            Color[] pixels = new Color[width * height];

            // Fill background
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = backgroundColor;
            }
            texture.SetPixels(pixels);

            // Draw points
            foreach (Vector2 point in points)
            {
                int x = Mathf.RoundToInt(point.x * width);
                int y = Mathf.RoundToInt(point.y * height);

                if (x >= 0 && x < width && y >= 0 && y < height)
                {
                    texture.SetPixel(x, y, pointColor);
                }
            }

            texture.Apply();
            return texture;
        }

        public static void SaveTextureToFile(Texture2D texture, string filePath, string format = "PNG")
        {
            byte[] bytes;
            if (format.ToUpper() == "PNG")
            {
                bytes = texture.EncodeToPNG();
            }
            else if (format.ToUpper() == "JPG" || format.ToUpper() == "JPEG")
            {
                bytes = texture.EncodeToJPG();
            }
            else
            {
                Debug.LogError("Unsupported image format: " + format);
                return;
            }

            File.WriteAllBytes(filePath, bytes);
            Debug.Log("Saved texture to: " + filePath);
        }

    }
}
