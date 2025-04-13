using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpellLibrary : MonoBehaviour
{
    [Tooltip("List of spell definitions. Each spell has a unique shape defined by a list of points.")]
    public List<SpellDefinition> spellDefinitions = new List<SpellDefinition>();

    public ShapeComparer shapeComparer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public SpellDefinition Search(List<Vector2> vectors)
    {
        if (vectors == null || vectors.Count == 0)
        {
            Debug.Log("No vectors provided.");
            return null;
        }
        if (this.shapeComparer == null)
        {
            Debug.Log("ShapeComparer is not set.");
            return null;
        }

        //foreach (SpellDefinition entry in spellDefinitions)
        //{
        //    if (this.shapeComparer.Compare(vectors, entry.points))
        //    {
        //        return entry;
        //    }
        //}
        //Vector2[] scaledVectors = Vector2Scaling.ScaleVectorsToFill(vectors.ToArray());
        //List<Vector2> scaledList = new List<Vector2>(scaledVectors);

        //SpellDefinition nearestMatch = this.shapeComparer.CompareAll(scaledList, spellDefinitions);

        SpellDefinition nearestMatch = this.shapeComparer.CompareAll(vectors, spellDefinitions);

        return nearestMatch;
    }
}
