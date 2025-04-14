using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class Vector2ListToImage : MonoBehaviour
    {
        public Image targetImage;
        public int textureWidth = 256;
        public int textureHeight = 256;
        //public Color lineColor = Color.black;

        public Color lineColorLeft = Color.yellow;
        public Color lineColorRight = Color.red;
        public Color lineColorSolo = Color.red;
        public float lineThickness = 2f;

        private Vector2 lastVector2Left; // Used to store the last vector2 for line drawing, if needed.  
        private Vector2 lastVector2Right; // Used to store the last vector2 for line drawing, if needed.  
        private Vector2 lastVector2Solo; // Used to store the last vector2 for line drawing, if needed.  

        public DebugTextDisplay debugTextDisplay;

        public void Start()
        {
            // Example list of Vector2 values. Replace with your actual data.
            List<Vector2> exampleVector2List = new List<Vector2>
            {
                new Vector2(0.5f, 0.5f),
                new Vector2(-0.5f, 0.5f),
                new Vector2(0f, -0.5f),
                new Vector2(0.2f, 0.8f),
                new Vector2(-0.9f, -0.3f),
            };

            //Display the example list.
            DisplayVector2List(exampleVector2List);
        }

        public void Update()
        {

        }

        public void DisplayVector2List(List<Vector2> vector2List)
        {
            if (targetImage == null)
            {
                Debug.LogError("Image is not assigned!");
                return;
            }

            if (vector2List == null || vector2List.Count == 0)
            {
                Debug.LogWarning("Vector2 list is empty or null.");
                targetImage.sprite = null; // Clear the image
                return;
            }

            Texture2D texture = new Texture2D(textureWidth, textureHeight);
            Color[] pixels = new Color[textureWidth * textureHeight];

            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = Color.black;
            }
            List<Vector2Int> pixelPoints = new List<Vector2Int>();
            foreach (Vector2 vector2 in vector2List)
            {
                Debug.Log(vector2);

                float x = (vector2.x + 1f) * 0.5f * textureWidth;
                float y = (vector2.y + 1f) * 0.5f * textureHeight;

                int pixelX = Mathf.Clamp(Mathf.RoundToInt(x), 0, textureWidth - 1);
                int pixelY = Mathf.Clamp(Mathf.RoundToInt(y), 0, textureHeight - 1);

                pixelPoints.Add(new Vector2Int(pixelX, pixelY)); // Store the pixel point for line drawing

                Debug.Log($"Pixel X: {pixelX}, Pixel Y: {pixelY}");

                pixels[pixelY * textureWidth + pixelX] = lineColorSolo;
            }

            texture.SetPixels(pixels);

            DrawLinesBetweenPoints(ref texture, pixelPoints, lineColorSolo); // Draw lines for left points

            texture.Apply();

            // Create a sprite from the texture and assign it to the Image component
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, textureWidth, textureHeight), Vector2.one * 0.5f);
            targetImage.sprite = sprite;
        }

        public void DisplayVector2List(List<Vector2> vector2ListLeft, List<Vector2> vector2ListRight)
        {
            if (targetImage == null)
            {
                Debug.LogError("Image is not assigned!");
                return;
            }

            if (vector2ListLeft == null || vector2ListLeft.Count == 0)
            {
                Debug.LogWarning("Vector2 list Left is empty or null.");
                targetImage.sprite = null; // Clear the image
                return;
            }

            if (vector2ListRight == null || vector2ListRight.Count == 0)
            {
                Debug.LogWarning("Vector2 list Right is empty or null.");
                targetImage.sprite = null; // Clear the image
                return;
            }

            Texture2D texture = new Texture2D(textureWidth, textureHeight);
            Color[] pixels = new Color[textureWidth * textureHeight];

            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = Color.black;
            }
            List<Vector2Int> pixelPointsLeft = new List<Vector2Int>();
            foreach (Vector2 vector2 in vector2ListLeft)
            {
                Debug.Log(vector2);

                //float x = (vector2.x + 1f) * 0.5f * textureWidth;
                //float y = (vector2.y + 1f) * 0.5f * textureHeight;

                float x = (vector2.x + 0.5f) * 0.5f * textureWidth;
                float y = (vector2.y + 0.5f) * 0.5f * textureHeight;

                int pixelX = Mathf.Clamp(Mathf.RoundToInt(x), 0, textureWidth - 1);
                int pixelY = Mathf.Clamp(Mathf.RoundToInt(y), 0, textureHeight - 1);

                pixelPointsLeft.Add(new Vector2Int(pixelX, pixelY)); // Store the pixel point for line drawing

                Debug.Log($"Pixel X: {pixelX}, Pixel Y: {pixelY}");

                pixels[pixelY * textureWidth + pixelX] = lineColorLeft;
                lastVector2Left = vector2; // Store the last vector2 for potential future use (like line drawing)   
            }
            List<Vector2Int> pixelPointsRight = new List<Vector2Int>();
            foreach (Vector2 vector2 in vector2ListRight)
            {
                Debug.Log(vector2);

                //float x = (vector2.x + 1f) * 0.5f * textureWidth;
                //float y = (vector2.y + 1f) * 0.5f * textureHeight;

                float x = (vector2.x + 0.5f) * 0.5f * textureWidth;
                float y = (vector2.y + 0.5f) * 0.5f * textureHeight;

                int pixelX = Mathf.Clamp(Mathf.RoundToInt(x), 0, textureWidth - 1);
                int pixelY = Mathf.Clamp(Mathf.RoundToInt(y), 0, textureHeight - 1);

                pixelPointsRight.Add(new Vector2Int(pixelX, pixelY)); // Store the pixel point for line drawing

                Debug.Log($"Pixel X: {pixelX}, Pixel Y: {pixelY}");

                pixels[pixelY * textureWidth + pixelX] = lineColorRight;
                lastVector2Right = vector2; // Store the last vector2 for potential future use (like line drawing)  
            }

            texture.SetPixels(pixels);

            DrawLinesBetweenPoints(ref texture, pixelPointsLeft, lineColorLeft); // Draw lines for left points
            DrawLinesBetweenPoints(ref texture, pixelPointsRight, lineColorRight); // Draw lines for right points   

            texture.Apply();

            // Create a sprite from the texture and assign it to the Image component
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, textureWidth, textureHeight), Vector2.one * 0.5f);
            targetImage.sprite = sprite;

            if (debugTextDisplay != null)
            {
                debugTextDisplay.UpdateDebugText(Common.GetOutputString(pixelPointsLeft, pixelPointsRight));
            }
        }

        private void DrawLinesBetweenPoints(ref Texture2D texture, List<Vector2Int> pixelPoints, Color lineColor)
        {
            // Draw lines between points
            for (int i = 0; i < pixelPoints.Count - 1; i++)
            {
                DrawLine(texture, pixelPoints[i], pixelPoints[i + 1], lineColor);
            }
        }

        public void DrawVector2List(List<Vector2> points)
        {
            if (targetImage == null || points == null || points.Count < 2)
            {
                Debug.LogError("Target Image or insufficient points provided.");
                return;
            }

            Texture2D texture = new Texture2D(textureWidth, textureHeight);
            Color[] pixels = new Color[textureWidth * textureHeight];

            // Clear texture
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = Color.clear;
            }
            texture.SetPixels(pixels);

            // Convert Vector2 points to pixel coordinates
            List<Vector2Int> pixelPoints = new List<Vector2Int>();
            foreach (Vector2 point in points)
            {
                int x = Mathf.RoundToInt(Mathf.Clamp01(point.x) * (textureWidth - 1));
                int y = Mathf.RoundToInt(Mathf.Clamp01(point.y) * (textureHeight - 1));
                pixelPoints.Add(new Vector2Int(x, y));
            }

            // Draw lines between points
            for (int i = 0; i < pixelPoints.Count - 1; i++)
            {
                DrawLine(texture, pixelPoints[i], pixelPoints[i + 1], lineColorSolo);
            }

            texture.Apply();
            targetImage.sprite = Sprite.Create(texture, new Rect(0, 0, textureWidth, textureHeight), Vector2.one * 0.5f);
        }

        //private void DrawLine(Texture2D texture, Vector2Int p1, Vector2Int p2, Color color)
        //{
        //    float deltaY = p2.y - p1.y;
        //    float deltaX = p2.x - p1.x;

        //    float slope = deltaY / (deltaX == 0 ? 0 : deltaX);

        //    Vector2Int vSlope = new Vector2Int((int)deltaX, (int)deltaY); // Initialize slope vector for line drawing   


        //    int px = p1.x; // Start from the first point    
        //    int py = p1.y; // Start from the first point    

        //    Vector2Int currentPoint = new Vector2Int();

        //    do
        //    {
        //        currentPoint = new Vector2Int(px, py); // Update the current point based on px and py

        //        Vector2Int lastPoint = new Vector2Int(currentPoint.x, currentPoint.y);

        //        // line = y = mx + b; where m is slope and b is y-intercept. We can use slope to find the next point in the line.
        //        if (p1.x < p2.x)
        //        {
        //            currentPoint += new Vector2Int(1, 0);
        //            currentPoint.y = (int)(p1.y + slope * (currentPoint.x - p1.x)); // Calculate the y value based on the slope and current x position. This is to ensure we follow the slope of the line.
        //        }
        //        else if (p1.x > p2.x)
        //        {
        //            currentPoint += new Vector2Int(-1, 0);
        //            currentPoint.y = (int)(p1.y + slope * (currentPoint.x - p1.x)); // Calculate the y value based on the slope and current x position. This is to ensure we follow the slope of the line.
        //        }
        //        else if (p1.x == p2.x)
        //        {
        //            // TODO: y-scale crawl
        //            if (p1.y < p2.y)
        //            {
        //                currentPoint += new Vector2Int(0, 1);
        //            }
        //            else if (p1.y > p2.y)
        //            {
        //                currentPoint += new Vector2Int(0, -1);
        //            }
        //        }

        //        bool isFinishedX = false;
        //        bool isFinishedY = false;
        //        if (p1.x < p2.x && currentPoint.x >= p2.x) // If we have reached or passed the end point in x direction, set to end point and break
        //        {
        //            isFinishedX = true;
        //        }
        //        else if (p1.x >= p2.x && currentPoint.x <= p2.x)
        //        {
        //            isFinishedX = true; // We have reached or passed the end point in x direction, set to end point and break   
        //        }
        //        if (p1.y < p2.y && currentPoint.y >= p2.y) // If we have reached or passed the end point in y direction, set to end point and break
        //        {
        //            isFinishedY = true;
        //        }
        //        else if (p1.y >= p2.y && currentPoint.y <= p2.y)
        //        {
        //            isFinishedY = true; // We have reached or passed the end point in y direction, set to end point and break   
        //        }
        //        if (isFinishedX && isFinishedY)
        //        {
        //            break; // Break the loop if we have reached the end point in both x and y directions
        //        }
        //        else
        //        {
        //            texture.SetPixel(currentPoint.x, currentPoint.y, color);
        //        }
        //    }
        //    while (true);

        //    texture.Apply(); // Apply the changes to the texture.

        //}

        private void DrawLine(Texture2D texture, Vector2Int p1, Vector2Int p2, Color color)
        {
            // Bresenham's Line Algorithm implementation
            int x0 = p1.x;
            int y0 = p1.y;
            int x1 = p2.x;
            int y1 = p2.y;

            int dx = Mathf.Abs(x1 - x0);
            int dy = Mathf.Abs(y1 - y0);
            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;
            int err = dx - dy;

            while (true)
            {
                if (x0 >= 0 && x0 < texture.width && y0 >= 0 && y0 < texture.height)
                {
                    texture.SetPixel(x0, y0, color);
                }

                if (x0 == x1 && y0 == y1) break;
                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }
            }

            texture.Apply(); // Apply the changes to the texture.
        }

        //private void DrawLine(Texture2D texture, Vector2Int p1, Vector2Int p2, Color color)
        //{
        //    int x0 = p1.x;
        //    int y0 = p1.y;
        //    int x1 = p2.x;
        //    int y1 = p2.y;

        //    int dx = Mathf.Abs(x1 - x0);
        //    int dy = Mathf.Abs(y1 - y0);
        //    int sx = x0 < x1 ? 1 : -1;
        //    int sy = y0 < y1 ? 1 : -1;
        //    int err = dx - dy;

        //    while (true)
        //    {
        //        DrawThickPixel(texture, x0, y0, color);



        //        if (x0 == x1 && y0 == y1) break;
        //        int e2 = 2 * err;
        //        if (e2 > -dy) { err -= dy; x0 += sx; }
        //        if (e2 < dx) { err += dx; y0 += sy; }
        //    }
        //}

        //private void DrawThickPixel(Texture2D texture, int x, int y, Color color)
        //{
        //    int thickness = Mathf.RoundToInt(lineThickness);
        //    for (int i = -thickness / 2; i <= thickness / 2; i++)
        //    {
        //        for (int j = -thickness / 2; j <= thickness / 2; j++)
        //        {
        //            int drawX = x + i;
        //            int drawY = y + j;

        //            if (drawX >= 0 && drawX < texture.width && drawY >= 0 && drawY < texture.height)
        //            {
        //                texture.SetPixel(drawX, drawY, color);
        //            }
        //        }
        //    }
        //}
    }
}
