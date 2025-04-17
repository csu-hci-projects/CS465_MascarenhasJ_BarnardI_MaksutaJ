using Assets;
using Meta.WitAi.CallbackHandlers;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows.Speech;

public class VoiceRecognizer : MonoBehaviour
{
    private string recognizedPhrase;

    private bool _voiceRecognized = false;
    private string _lastRecognizedPhrase = string.Empty;    

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

    public void Reset()
    {
        this.recognizedPhrase = string.Empty;
        this._voiceRecognized = false;
    }
}
