using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskRecognizer : MonoBehaviour
{
    public OpenXRVoiceRecognizer voiceRecognizer; // Reference to the VoiceRecognizer component
    public GestureRecognizer gestureRecognizer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool voiceRecognized = false;
        bool gestureRecognized = false;

        if (voiceRecognizer != null)
        {
            voiceRecognized = voiceRecognizer.IsPhraseRecognized();
        }
        if (gestureRecognizer != null)
        {
            gestureRecognized = gestureRecognizer.IsGestureRecognized();
        }
        // Check if the voice recognizer has recognized a phrase
        if (voiceRecognized && gestureRecognized)
        {
            KeyValuePair<string, Action> recognizedPhrase = voiceRecognizer.GetRecognizedPhrase();
            KeyValuePair<string, Action> recognizedGesture = gestureRecognizer.GetRecognizedGesture();

            if (recognizedPhrase.Key == recognizedGesture.Key)
            {
                // Perform the task based on the recognized phrase and gesture
                PerformTask(recognizedPhrase.Value);
            }   
        }
    }

    private void PerformTask(Action v)
    {
        v.Invoke();
    }
}
