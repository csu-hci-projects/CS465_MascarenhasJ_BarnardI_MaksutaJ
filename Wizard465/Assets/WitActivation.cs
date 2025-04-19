using Oculus.Voice;
using UnityEngine;

public class WitActivation : MonoBehaviour
{
    private AppVoiceExperience _voiceExperience;
    private void OnValidate()
    {
        if (!_voiceExperience) _voiceExperience = GetComponent<AppVoiceExperience>();
    }

    private void Start()
    {
        _voiceExperience = GetComponent<AppVoiceExperience>();
    }

    private void Update()
    {
        try
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("*** Pressed Space bar ***");
                ActivateWit();
            }
        }
        catch (System.Exception)
        {
            // do nothing.
        }
    }

    /// <summary>
    /// Activates Wit i.e. start listening to the user.
    /// </summary>
    public void ActivateWit()
    {
        _voiceExperience.Activate();
    }
}
