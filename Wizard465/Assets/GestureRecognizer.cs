using System;
using System.Collections.Generic;
using UnityEngine;

public class GestureRecognizer : MonoBehaviour
{
    private string recognizedGesture;
    private DateTime _recognizedTime = DateTime.MinValue;
    private bool _gestureRecognized = false;
    private string _lastRecognizedGesture = string.Empty;
    private DateTime _lastRecognizedTime = DateTime.MinValue;
    
    public DateTime RecognizedTime
    {
        get { return _recognizedTime; }
        set
        {
            this.LastRecognizedTime = _recognizedTime; // Store the last recognized time before updating
            _recognizedTime = value;
        }
    }

    private DateTime LastRecognizedTime
    {
        get { return _lastRecognizedTime; }
        set { _lastRecognizedTime = value; }
    }

    public DebugTextDisplay debugTextDisplay;
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
        this.RecognizedTime = DateTime.Now; // Set the recognized time to the current time
        this._lastRecognizedGesture = recognizedGesture; // Store the last recognized gesture

        if (debugTextDisplay != null)
        {
            debugTextDisplay.UpdateDebugText(string.Format("Gesture recognized is {0}.", recognizedGesture));
        }

        Debug.Log("OnVoiceRecognized called with: " + recognizedGesture);
    }

    internal bool IsGestureRecognized()
    {
        return !string.IsNullOrEmpty(this.recognizedGesture);
    }

    internal string GetRecognizedGesture()
    {
        return this.recognizedGesture; // "fireball"; // TODO: recognizedGesture;
    }

    public void Reset()
    {
        this.recognizedGesture = string.Empty;
        this.RecognizedTime = DateTime.MinValue;
        this.LastRecognizedTime = DateTime.MinValue;
        this._gestureRecognized = false;
    }
}
