using Meta.WitAi.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class ShapePatternFile : MonoBehaviour
{
    [Tooltip("The file containing the shape pattern data. this is optional, but overrides the vectors property with the vectors from the file.")]
    public TextAsset file;
    [Tooltip("The vectors for the shape. this property will be overrided by the file provided, static values should use null file.")]
    public List<Vector2> vectors;
    public ShapePatternFile(List<Vector2> vectors)
    {
        this.vectors = vectors;
    }
    public ShapePatternFile(ShapePatternFile shapePatternFile)
    {
        this.vectors = shapePatternFile.vectors;
    }

    public ShapePatternFile(ShapePatternData shapePatternData)
    {
        this.vectors = shapePatternData.vectors;
    }
    public ShapePatternFile(string jsonString) : this(JsonUtility.FromJson<ShapePatternData>(jsonString))
    {
    }

    public void Start()
    {
        if (file != null)
        {
            string jsonString = file.text;
            ShapePatternFile shapePatternFile = new ShapePatternFile(jsonString);
            this.vectors = shapePatternFile.vectors;
        }
    }

    public void Update()
    {

    }   
}
