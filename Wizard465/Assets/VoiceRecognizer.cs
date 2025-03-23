using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceRecognizer : MonoBehaviour
{
    // Array of words/phrases to recognize
    public string[] keywords = new string[] { "fireball", "smoke puff" };

    // GameObjects to spawn
    public GameObject fireballPrefab;
    public GameObject smokePuffPrefab;

    // Confidence level for triggering the event
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;

    // Where to spawn the objects
    public Transform spawnPoint;

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, System.Action> keywordActions = new Dictionary<string, System.Action>();

    private KeyValuePair<string, System.Action> recognizedPhrase;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Populate the dictionary of actions
        keywordActions.Add("fireball", SpawnFireball);
        keywordActions.Add("smoke puff", SpawnSmokePuff);

        // Initialize the keyword recognizer
        keywordRecognizer = new KeywordRecognizer(keywords, confidence);
        keywordRecognizer.OnPhraseRecognized += OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Phrase recognized: " + args.text);

        this.recognizedPhrase = new KeyValuePair<string, Action>(args.text, keywordActions[args.text]);

        if (keywordActions.TryGetValue(args.text, out System.Action action))
        {
            //action.Invoke();
        }
        else
        {
            Debug.LogWarning("Action not found for phrase: " + args.text);
        }

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

    internal KeyValuePair<string, System.Action> GetRecognizedPhrase()
    {
        return recognizedPhrase;
    }
}
