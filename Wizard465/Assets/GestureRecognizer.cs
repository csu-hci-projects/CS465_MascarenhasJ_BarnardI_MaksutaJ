using System.Collections.Generic;
using UnityEngine;

public class GestureRecognizer : MonoBehaviour
{
    private string recognizedGesture;

    private bool _gestureRecognized = false;
    private string _lastRecognizedGesture = string.Empty;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnGestureRecognized(string recognizedGesture)
    {
        if (string.IsNullOrEmpty(recognizedGesture))
        {
            Debug.LogWarning("No recognized text provided.");
            return;
        }

        this.recognizedGesture = recognizedGesture; // Update the recognized phrase
        this._lastRecognizedGesture = recognizedGesture; // Store the last recognized gesture

        Debug.Log("OnVoiceRecognized called with: " + recognizedGesture);
    }

    internal bool IsGestureRecognized()
    {
        return (true);
    }

    internal string GetRecognizedGesture()
    {
        return "fireball"; // TODO: recognizedGesture;
    }

    public void Reset()
    {
        this.recognizedGesture = string.Empty;
        this._gestureRecognized = false;
    }
}
