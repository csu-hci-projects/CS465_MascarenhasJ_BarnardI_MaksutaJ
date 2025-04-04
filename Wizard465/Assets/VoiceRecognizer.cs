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
    // Array of words/phrases to recognize
    public string[] keywords = new string[] { "fireball", "smoke" };

    // GameObjects to spawn
    public GameObject fireballPrefab;
    public GameObject smokePuffPrefab;

    // Confidence level for triggering the event
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;

    // Where to spawn the objects
    public Transform spawnPoint;

    private KeywordRecognizer keywordRecognizer;
    public Dictionary<string, System.Action> keywordActions = new Dictionary<string, System.Action>();

    //private KeyValuePair<string, System.Action> recognizedPhrase;
    private string recognizedPhrase;

    private MonoBehaviour currentBehavior; // Store the active behavior

    public TaskRecognizer taskRecognizer; // Reference to the TaskRecognizer component, if needed

    private bool _voiceRecognized = false;
    private string _lastRecognizedPhrase = string.Empty;    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //#if UNITY_STANDALONE_WIN
        //        currentBehavior = gameObject.AddComponent<DotNetVoiceRecognizer>();
        //#elif UNITY_STANDALONE_OSX
        //        currentBehavior = gameObject.AddComponent<OpenXRVoiceRecognizer>();
        //#elif UNITY_STANDALONE_LINUX
        //        currentBehavior = gameObject.AddComponent<OpenXRVoiceRecognizer>();
        //#else
        //        Debug.LogWarning("Unsupported platform.");
        //#endif
    }

    // Update is called once per frame
    void Update()
    {
        //#if UNITY_STANDALONE_WIN
        //            if (currentBehavior != null)
        //            {
        //                currentBehavior.Update();   
        //                Debug.Log("Windows Update");
        //            }
        //#elif UNITY_STANDALONE_OSX
        //            if (currentBehavior != null)
        //            {
        //                currentBehavior.Update();
        //                Debug.Log("Mac Update");
        //            }
        //#elif UNITY_STANDALONE_LINUX
        //            if (currentBehavior != null)
        //            {
        //                currentBehavior.Update();
        //                Debug.Log("Linux Update");
        //            }
        //#else
        //        // Optional: Handle unsupported platforms in Update.
        //#endif
    }

    public void OnVoiceRecognized(string recognizedText)
    {
        if (string.IsNullOrEmpty(recognizedText))
        {
            Debug.LogWarning("No recognized text provided.");
            return;
        }

        this.recognizedPhrase = recognizedText; // Update the recognized phrase
        //taskRecognizer.OnVoiceRecognized(recognizedText); // Call the TaskRecognizer's method to handle the recognized phrase, if available

        //if (this.eventPairs == null)
        //{
        //    Debug.LogWarning("eventPairs list is null. Ensure it is initialized properly before calling OnVoiceRecognized.");
        //    return;
        //}
        //foreach (EventPairs aPair in eventPairs)
        //{
        //    if (aPair.keyword == recognizedText)
        //    {
        //        //if (pairs.action != null)
        //        //{
        //        //    pairs.action.Invoke(); // Invoke the action associated with the keyword
        //        //}
        //        if (aPair.theEvent != null)
        //        {
        //            aPair.theEvent.Invoke(new string[] { recognizedText }); // Invoke the UnityEvent with the recognized text

        //            //return; // Exit after invoking to avoid multiple calls
        //        }
        //    }
            //if (keywordActions.TryGetValue(recognizedText, out System.Action action))
            //{
            //    action.Invoke();
            //}
            //else
            //{
            //    Debug.LogWarning("Action not found for phrase: " + recognizedText);
            //}
        //}
        Debug.Log("OnVoiceRecognized called with: " + recognizedText);
    }

    void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Phrase recognized: " + args.text);

        //this.recognizedPhrase = new KeyValuePair<string, Action>(args.text, keywordActions[args.text]);

        //if (keywordActions.TryGetValue(args.text, out System.Action action))
        //{
        //    //action.Invoke();
        //}
        //else
        //{
        //    Debug.LogWarning("Action not found for phrase: " + args.text);
        //}

    }

    void SpawnFireball()
    {
        if (fireballPrefab != null && spawnPoint != null)
        {
            Instantiate(fireballPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogError("Fireball prefab or spawn point not assigned.");
        }
    }

    void SpawnSmokePuff()
    {
        if (smokePuffPrefab != null && spawnPoint != null)
        {
            Instantiate(smokePuffPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogError("Smoke puff prefab or spawn point not assigned.");
        }
    }

    void OnDestroy()
    {
        if (keywordRecognizer != null && keywordRecognizer.IsRunning)
        {
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
        }
    }

    internal bool IsPhraseRecognized()
    {
        return ((object)recognizedPhrase != null);
    }

    internal string GetRecognizedPhrase()
    {
        return recognizedPhrase;
    }
}
