using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TouchpadInput : MonoBehaviour
{
    public TMP_InputField textField;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string text)
    {
        if (textField != null)
        {
            textField.text += text;
        }
        else
        {
            Debug.LogError("Text field is not assigned.");
        }
        // Log the text to the console
        Debug.Log("SetText called with: " + text);
    }

    public void ClearText()
    {
        if (textField != null)
        {
            textField.text = string.Empty;
        }
        else
        {
            Debug.LogError("Text field is not assigned.");
        }
        // Log the text to the console
        Debug.Log("ClearText called");
    }   
}
