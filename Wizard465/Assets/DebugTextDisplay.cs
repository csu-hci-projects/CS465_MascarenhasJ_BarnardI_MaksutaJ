using TMPro;
using UnityEngine;

public class DebugTextDisplay : MonoBehaviour
{
    public TMP_Text debugText; // Reference to the TextMeshProUGUI component    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (debugText == null)
        {
            //debugText = GetComponent<TMP_Text>();
            enabled = false;
            return;
        }

        debugText.text = "Debug Output:";
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = Time.time;
        UpdateDebugText("Time:" + currentTime.ToString("F2") + " s");
    }

    public void UpdateDebugText(string debugText)
    {
        if (this.debugText != null)
        {
            this.debugText.text = "DEBUG:\n" + debugText;
        }
    }

    public void AppendDebugText(string debugText)
    {
        if (this.debugText != null)
        {
            this.debugText.text += "\n" + debugText;
        }
    }

    public void ClearDebugText(string debugText)
    {
        if (this.debugText != null)
        {
            this.debugText.text += "DEBUG:\n";
        }
    }

    void HandleOnDisplayText(string text)
    {
        UpdateDebugText(text);
    }

}
