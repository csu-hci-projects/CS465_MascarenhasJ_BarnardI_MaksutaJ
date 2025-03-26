using System.Collections.Generic;
using UnityEngine;

public class OpenXRVoiceRecognizer : MonoBehaviour
{
    public string[] keywords = new string[] { "fireball", "smoke puff" };
    public GameObject fireballPrefab;
    public GameObject smokePuffPrefab;
    public Transform spawnPoint;

    private Dictionary<string, System.Action> keywordActions = new Dictionary<string, System.Action>();

    private KeyValuePair<string, System.Action> recognizedPhrase;

#if UNITY_ANDROID && !UNITY_EDITOR
    private VoiceService voiceService;
#endif


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keywordActions.Add("fireball", SpawnFireball);
        keywordActions.Add("smoke puff", SpawnSmokePuff);

#if UNITY_ANDROID && !UNITY_EDITOR
        voiceService = VoiceService.Instance;
        voiceService.AddListener(OnVoiceServiceEvent);
        voiceService.BeginVoiceCapture();
#endif    
    }

    // Update is called once per frame
    void Update()
    {

    }

#if UNITY_ANDROID && !UNITY_EDITOR
    public void OnVoiceServiceEvent(VoiceServiceEvent voiceEvent)
    {
        if (voiceEvent.Id == VoiceServiceEventID.Transcription)
        {
            string recognizedText = voiceEvent.Args.GetString("text");
            Debug.Log("Recognized: " + recognizedText);

            this.recognizedPhrase = new KeyValuePair<string, Action>(args.text, keywordActions[args.text]);

            if (keywordActions.TryGetValue(recognizedText.ToLower(), out System.Action action))
            {
                action.Invoke();
            }
        }
        if (voiceEvent.Id == VoiceServiceEventID.Error)
        {
            Debug.LogError("Voice Service Error: " + voiceEvent.Args.GetString("error"));
        }
    }
#endif

    public void SpawnFireball()
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

    public void SpawnSmokePuff()
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
#if UNITY_ANDROID && !UNITY_EDITOR
        if (voiceService != null)
        {
            voiceService.EndVoiceCapture();
            voiceService.RemoveListener(OnVoiceServiceEvent);
        }
#endif
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
