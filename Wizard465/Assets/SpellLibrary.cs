using Assets;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class SpellLibrary : MonoBehaviour
{
    [Tooltip("List of spell definitions. Each spell has a unique shape defined by a list of points.")]
    public List<SpellDefinition> spellDefinitions = new List<SpellDefinition>();

    public ShapeComparer shapeComparer;

    private int currentSpellIndex = -1;

    public UnityEngine.UI.Image spellBookImageDisplay;
    public TMP_Text spellBookTextTitleDisplay;
    public TMP_Text spellBookTextVocalComponentDisplay;
    public Vector2ListToImage vectorListToImageConverter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentSpellIndex == -1 && spellDefinitions.Count > 0)
        {
            currentSpellIndex = 0;
        }
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
        //Vector2[] scaledVectors = Vector2Scaling.ScaleByMinMax(vectors);
        //List<Vector2> scaledList = new List<Vector2>(scaledVectors);

        //SpellDefinition nearestMatch = this.shapeComparer.CompareAll(scaledList, spellDefinitions);

        SpellDefinition nearestMatch = this.shapeComparer.CompareAll(vectors, spellDefinitions);

        return nearestMatch;
    }

    private void displaySpellInSpellBook()
    {
        if (this.spellBookImageDisplay != null)
        {
            vectorListToImageConverter.DisplayVector2List(this.spellDefinitions[currentSpellIndex].points);
        }
        if (this.spellBookTextTitleDisplay != null)
        {
            this.spellBookTextTitleDisplay.text = this.spellDefinitions[currentSpellIndex].Name;
        }
        if (this.spellBookTextVocalComponentDisplay != null)
        {
            this.spellBookTextVocalComponentDisplay.text = this.spellDefinitions[currentSpellIndex].SpeechComponent;
        }
    }

    public void NextSpell()
    {
        currentSpellIndex += 1;
        if (currentSpellIndex > spellDefinitions.Count - 1)
        {
            currentSpellIndex = 0;
        }
        displaySpellInSpellBook();  
    }

    public void PreviousSpell()
    {
        currentSpellIndex -= 1;
        if (currentSpellIndex < 0)
        {
            currentSpellIndex = spellDefinitions.Count - 1;
        }
        displaySpellInSpellBook();
    }

}
