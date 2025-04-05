using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TaskRecognizer : MonoBehaviour
{
    public VoiceRecognizer voiceRecognizer; // Reference to the VoiceRecognizer component
    public GestureRecognizer gestureRecognizer;

    public List<EventPairs> eventPairs = new List<EventPairs>();

    [Serializable]
    public class EventPairs
    {
        public string keyword;
        public UnityEvent<string[]> theEvent;

        public EventPairs(string keyword, UnityEvent<string[]> theEvent)
        {
            this.keyword = keyword;
            this.theEvent = theEvent;
        }
    }

    public void OnTaskRecognized(string recognizedText)
    {
        if (string.IsNullOrEmpty(recognizedText))
        {
            Debug.LogWarning("No recognized text provided.");
            return;
        }
        if (this.eventPairs == null)
        {
            Debug.LogWarning("eventPairs list is null. Ensure it is initialized properly before calling OnVoiceRecognized.");
            return;
        }
        foreach (EventPairs aPair in eventPairs)
        {
            if (aPair.keyword == recognizedText)
            {
                if (aPair.theEvent != null)
                {
                    aPair.theEvent.Invoke(new string[] { recognizedText }); // Invoke the UnityEvent with the recognized text
                }
            }
        }
        Debug.Log("OnVoiceRecognized called with: " + recognizedText);
    }

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
            string recognizedPhrase = voiceRecognizer.GetRecognizedPhrase();
            string recognizedGesture = gestureRecognizer.GetRecognizedGesture();

            if (recognizedPhrase == recognizedGesture)
            {
                // Perform the task based on the recognized phrase and gesture
                OnTaskRecognized(recognizedPhrase);
            }   
        }
    }

    internal void OnVoiceRecognized(string recognizedText)
    {
        throw new NotImplementedException();
    }
}
