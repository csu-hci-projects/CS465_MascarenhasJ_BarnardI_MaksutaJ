using System;
using UnityEngine;

public class VoiceRecognizer : MonoBehaviour
{
    private string recognizedPhrase;

    private DateTime _recognizedTime = DateTime.MinValue;
    private DateTime _lastRecognizedTime = DateTime.MinValue;
    private bool _voiceRecognized = false;
    private string _lastRecognizedPhrase = string.Empty;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnVoiceRecognized(string recognizedText)
    {
        if (string.IsNullOrEmpty(recognizedText))
        {
            Debug.LogWarning("No recognized text provided.");
            return;
        }

        this.recognizedPhrase = recognizedText; // Update the recognized phrase
        this.RecognizedTime = DateTime.Now; // Set the recognized time to the current time
        this._lastRecognizedPhrase = recognizedText;

        Debug.Log("OnVoiceRecognized called with: " + recognizedText);
    }

    internal bool IsPhraseRecognized()
    {
        return (!string.IsNullOrEmpty(recognizedPhrase)); // (object)recognizedPhrase != null);
    }

    internal string GetRecognizedPhrase()
    {
        return recognizedPhrase;
    }

    internal string GetRecognizedPhraseTime()
    {
        return recognizedPhrase;
    }

    public void Reset()
    {
        this.recognizedPhrase = string.Empty;
        this.RecognizedTime = DateTime.MinValue;
        this.LastRecognizedTime = DateTime.MinValue;
        this._voiceRecognized = false;
    }
}
