using System.Collections.Generic;
using UnityEngine;

public class GestureRecognizer : MonoBehaviour
{
    private KeyValuePair<string, System.Action> recognizedGesture;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Check if a gesture is recognized
        if (IsGestureRecognized())
        {
            if (((object)recognizedGesture) != null)
            {
                // Perform the action associated with the recognized gesture
                recognizedGesture.Value.Invoke();
            }
        }
    }


    internal bool IsGestureRecognized()
    {
        return (true);
    }

    internal KeyValuePair<string, System.Action> GetRecognizedGesture()
    {
        return recognizedGesture;
    }
}
