using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using System.Linq;

public static class Vector2Scaling
{
    public static Vector2[] ScaleVectorsToFill(Vector2[] vectors)
    {
        if (vectors == null || vectors.Length == 0)
        {
            return vectors; // Or handle the empty case as needed
        }

        // Find the bounding box of the vectors
        Vector2[] minMax = FindMinMax(vectors.ToArray());

        float minX = minMax[0].x;
        float maxX = minMax[1].x;   
        float minY = minMax[0].y;   
        float maxY = minMax[1].y;   

        float width = maxX - minX;
        float height = maxY - minY;

        if (width == 0 && height == 0)
        {
            // All points are the same, no scaling needed.
            return vectors;
        }

        float scaleX = 2f / width; // Scale to fill [-1, 1] horizontally
        float scaleY = 2f / height; // Scale to fill [-1, 1] vertically

        float scale = Mathf.Min(scaleX, scaleY); // Use the smaller scale to maintain aspect ratio

        // Calculate the center of the original bounding box
        Vector2 center = new Vector2((minX + maxX) / 2f, (minY + maxY) / 2f);

        Vector2[] scaledVectors = new Vector2[vectors.Length];
        for (int i = 0; i < vectors.Length; i++)
        {
            // Translate the point to the origin, scale, then translate back to the center of the target [-1,1] square.
            scaledVectors[i] = (vectors[i] - center) * scale;
        }

        return scaledVectors;
    }

    public static Vector2[] ScaleByMinMax(List<Vector2> vectors)
    {
        Vector2 newMax = new Vector2(1f, 1f);
        Vector2 newMin = new Vector2(-1.0f, -1.0f);
        return ScaleByMinMax(vectors, newMax, newMin);
    }

    public static Vector2[] ScaleByMinMax(List<Vector2> vectors, Vector2 newMax, Vector2 newMin)
    {
        Vector2[] minMax = FindMinMax(vectors.ToArray());

        Vector2 min = minMax[0];
        Vector2 max = minMax[1];

        //rescaled_coordinate = (coordinate - min_coordinate) / (max_coordinate - min_coordinate) * (new_max - new_min) + new_min
        for (int i = 0; i < vectors.Count; i++)
        {
            vectors[i] = (vectors[i] - min) / (max - min) * (newMax - newMin) + newMin;
        }       
        return vectors.ToArray();
    }

    public static Vector2[] FindMinMax(Vector2[] vectors)
    {
        float minX = float.MaxValue;
        float maxX = float.MinValue;
        float minY = float.MaxValue;
        float maxY = float.MinValue;

        // Find the bounding box of the vectors
        foreach (Vector2 v in vectors)
        {
            minX = Mathf.Min(minX, v.x);
            maxX = Mathf.Max(maxX, v.x);
            minY = Mathf.Min(minY, v.y);
            maxY = Mathf.Max(maxY, v.y);
        }

        return new Vector2[] { new Vector2(minX, minY), new Vector2(maxX, maxY) };  
    }   

}