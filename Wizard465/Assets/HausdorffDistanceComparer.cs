using System;
using System.Collections.Generic;
using UnityEngine;

public class HausdorffDistanceComparer : ShapeComparer
{
    public override bool Compare(List<Vector2> shape1, List<Vector2> shape2, double threshhold)
    {
        bool result = false;
        double distance = HausdorffDistance(shape1, shape2);
        if (distance < threshhold)
        {
            result = true;
        }
        return result;
    }

    public override SpellDefinition CompareAll(List<Vector2> testPoints, List<SpellDefinition> library)
    {
        double maxDistance = double.MaxValue;
        SpellDefinition closestSpell = null;
        foreach (SpellDefinition entry in library)
        {
            double distance = HausdorffDistance(testPoints, entry.points);
            if (distance < maxDistance)
            {
                maxDistance = distance;
                closestSpell = entry;
            }
        }
        return closestSpell;
    }

    public double HausdorffDistance(List<Vector2> drawingA, List<Vector2> drawingB)
    {
        double directed1to2 = DirectedHausdorff(drawingA, drawingB);
        double directed2to1 = DirectedHausdorff(drawingB, drawingA);

        return Math.Max(directed1to2, directed2to1);
    }

    private double DirectedHausdorff(List<Vector2> drawingA, List<Vector2> drawingB)
    {
        double maxDistance = 0.0f;

        foreach (Vector2 pointA in drawingA)
        {
            double minDistance = float.MaxValue;

            foreach (Vector2 pointB in drawingB)
            {
                double distance = EuclideanDistance(pointA, pointB);
                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }

            if (minDistance > maxDistance)
            {
                maxDistance = minDistance;
            }
        }

        return maxDistance;
    }

    private double EuclideanDistance(Vector2 point1, Vector2 point2)
    {
        double deltaX = point1.x - point2.x;
        double deltaY = point1.y - point2.y;
        return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
    }

}
