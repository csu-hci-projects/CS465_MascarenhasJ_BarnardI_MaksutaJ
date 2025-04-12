using System;
using TMPro;
using UnityEngine;

public class GameHUD : MonoBehaviour
{
    public Game game;
    //public Transform headTransform;
    //public Canvas canvas;

    public TMP_Text latinSquareCodeText;
    public TMP_Text inputMethodText;

    private Transform theTransform;


    private TouchScreenKeyboard overlayKeyboard;
    public static string inputText = "";

    public GameObject LatinSquareCodeInputPanel;

    public TMP_InputField latinSquareCodeInputField;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.game = Game.Instance;

        theTransform = this.gameObject.GetComponent<Transform>();
        setTransformToHead();
    }

    // Update is called once per frame
    void Update()
    {
        setTransformToHead();
        showLatinSquareCodeInputPanel();
    }

    private void showLatinSquareCodeInputPanel()
    {
        LatinSquareCodeInputPanel.SetActive((game.latinSquareCode == string.Empty));
    }

    private void setTransformToHead()
    {
        //if (theTransform != null && headTransform != null)
        //{
        //    theTransform.position = headTransform.position;
        //    theTransform.rotation = headTransform.rotation;
        //}
        setCanvasTransformToHead();
        setUIElements();
    }
    private void setCanvasTransformToHead()
    {
        //if (canvas != null && headTransform != null)
        //{
        //    canvas.transform.position = headTransform.position;
        //    canvas.transform.rotation = headTransform.rotation;
        //}
        setUIElements();
    }

    private void setUIElements()
    {
        if (game != null)
        {
            setLatinSquareCodeText(game.latinSquareCode);
            setInputMethodText(game.inputMethod.ToString());
        }
    }

    private void setLatinSquareCodeText(string text)
    {
        if (latinSquareCodeText != null)
        {
            latinSquareCodeText.text = text;
        }
    }

    private void setInputMethodText(string text)
    {
        if (inputMethodText != null)
        {
            inputMethodText.text = text;
        }
    }


    void ShowMetaQuestKeyboard()
    {
        //// Display the system keyboard
        //OVRManager.InputTextOptions options = new OVRManager.InputTextOptions();
        //options.SetTitle("Enter Text");
        //options.SetDescription("Please enter your text.");
        //options.SetInputMode(OVRManager.InputTextMode.Any);

        //OVRManager.InputText(options, (Message<string> msg) =>
        //{
        //    if (msg.IsError)
        //    {
        //        Debug.LogError("Error displaying keyboard: " + msg.GetError());
        //    }
        //    else
        //    {
        //        // Keyboard input received
        //        string inputText = msg.Data;
        //        inputField.text = inputText;
        //        //Move the cursor to the end of the input field
        //        inputField.MoveTextEnd(false);
        //    }
        //});
    }

    public void ShowKeyboard() 
    {
        overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        
    }

    public void GetKeyboardText()
    {
        if (overlayKeyboard != null)
        {
            inputText = overlayKeyboard.text;
        }
    }

    public void SetLatinSquareCode()
    {
        if (this.latinSquareCodeInputField != null)
        {
            game.latinSquareCode = this.latinSquareCodeInputField.text;
        }   
    }

}
