using TMPro;
using UnityEngine;

public class GameHUD : MonoBehaviour
{
    public GameReference gameReference;
    private Game game { 
        get {
            Game result = null;
            if (gameReference != null)
            {
                result = gameReference.theGame;
            }
            else
            {
                result = Game.Instance;
            }
            return result;
        } 
    }

    public GameObject HUDCanvas;
    public TMP_Text latinSquareCodeText;
    public TMP_Text inputMethodText;
    public TMP_Text currentLevelText;

    private Transform theTransform;


    private TouchScreenKeyboard overlayKeyboard;
    public static string inputText = "";

    public GameObject LatinSquareCodeInputPanel;

    public TMP_InputField latinSquareCodeInputField;

    public GameObject GameOverCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //this.game = Game.Instance;

        theTransform = this.gameObject.GetComponent<Transform>();
        setTransformToHead();
    }

    // Update is called once per frame
    void Update()
    {
        setTransformToHead();

        if (game.gameState == Game.GameState.Playing)
        {
            showHUDCanvas();
        }
        else
        {
            hideHUDCanvas();
        }
        if (game.gameState == Game.GameState.GameOver)
        {
            ShowGameOverCanvas();
        }
        else
        {
            HideGameOverCanvas();   
        }
        showLatinSquareCodeInputPanel();
    }

    public void ShowGameOverCanvas()
    {
        if (GameOverCanvas != null)
        {
            GameOverCanvas.SetActive(true);
        }
    }

    private void HideGameOverCanvas()
    {
        if (GameOverCanvas != null)
        {
            GameOverCanvas.SetActive(false);
        }
    }   

    private void showHUDCanvas()
    {
        if (HUDCanvas != null)
        {
            HUDCanvas.SetActive(true);
        }
    }

    private void hideHUDCanvas()
    {
        if (HUDCanvas != null)
        {
            HUDCanvas.SetActive(false);
        }
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
            setCurrentLevelText((game.currentLevelIndex + 1).ToString());
        }
    }

    private void setCurrentLevelText(string text)
    {
        if (currentLevelText != null)
        {
            currentLevelText.text = text;
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
        game.LoadLatinSquareCode();
        game.StartGame();
    }

    public void NextLevel()
    {
        game.NextLevel();
    }

}
