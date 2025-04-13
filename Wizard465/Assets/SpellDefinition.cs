using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpellDefinition : MonoBehaviour
{

    public string Name;
    public string SpeechComponent;
    public List<Vector2> points;
    public ShapePatternFile shapePatternFile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (shapePatternFile != null && (this.points == null || this.points.Count == 0))
        {
            this.points = shapePatternFile.vectors;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
