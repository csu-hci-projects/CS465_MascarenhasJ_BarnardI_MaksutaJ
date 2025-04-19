using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Android.Types;
using UnityEditor.XR.Interaction.Toolkit.Locomotion.Teleportation;
using UnityEngine;
using static TaskRecognizer;

public class Game
{

    private static Game instance;

    public static Game Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Game();
            }
            return instance;
        }
    }

    public enum GameState
    {
        MainMenu,
        Playing,
        GameOver
    }

    public enum InputMethod
    {
        [Description("None")]
        None = 0,
        [Description("Controller")]
        Controller = 1,
        [Description("Hands")]
        Hands = 2,
        [Description("Pen")]
        Pen = 3
    }

    /// <summary>
    /// Returns the current state of the game.
    /// </summary>
    public GameState gameState;

    /// <summary>
    /// Returns the input method for the current level.
    /// </summary>
    public InputMethod inputMethod
    {
        get
        {
            InputMethod result = InputMethod.None;
            if (gameState == GameState.Playing)
            {
                result = gameLevels[currentLevelIndex].InputMethod;
            }
            return result;
        }
    }

    /// <summary>
    /// Returns the current latin square value for the game.
    /// </summary>
    /// <returns></returns>
    public int CurrentLatinSquareValue()
    {
        int result = -1;
        if (gameLevels != null && gameLevels.Count > 0 && currentLevelIndexInRange)
        {
            result = gameLevels[currentLevelIndex].LatinSquareValue;
        }
        return result;
    }

    private bool currentLevelIndexInRange
    {
        get { return currentLevelIndex > -1 && currentLevelIndex < gameLevels.Count; }
    }

    /// <summary>
    /// Returns the latin square code for the game.
    /// </summary>
    public string latinSquareCode;

    /// <summary>
    /// Returns the list of game levels.
    /// </summary>
    public List<GameLevel> gameLevels = new List<GameLevel>();

    /// <summary>
    /// Returns the index of the current level.
    /// </summary>
    public int currentLevelIndex = -1;

    public Game()
    {
        gameState = GameState.MainMenu;
        latinSquareCode = string.Empty;

        gameLevels = new List<GameLevel>();
        currentLevelIndex = -1;
    }

    public GameLevel currentLevel
    {
        get
        {
            GameLevel result = null;
            if (currentLevelIndex >= 0 && currentLevelIndex < gameLevels.Count)
            {
                result = gameLevels[currentLevelIndex];
            }
            return result;
        }
    }

    public InputMethod GetInputMethodFromLatinSquareDigit(int latinSquareDigit)
    {
        if (Enum.IsDefined(typeof(InputMethod), latinSquareDigit))
        {
            return (InputMethod)Enum.ToObject(typeof(InputMethod), latinSquareDigit);
        }
        else
        {
            return Enum.GetValues(typeof(InputMethod)).Cast<InputMethod>().FirstOrDefault();
        }
    }

    public void LoadLatinSquareCode()
    {
        if (latinSquareCode.Length > 0)
        {
            for (int n = 0; n < latinSquareCode.Length; n++)
            {
                int latinsquareDigit = int.Parse(latinSquareCode.ToCharArray()[n].ToString());
                InputMethod inputMethod = GetInputMethodFromLatinSquareDigit(latinsquareDigit);

                GameLevel gameLevel = new GameLevel(latinsquareDigit, inputMethod);
                this.gameLevels.Add(gameLevel);
            }
        }
    }

    public void StartGame()
    {
        if (gameLevels.Count > 0)
        {
            gameState = GameState.Playing;
            currentLevelIndex = 0;
            gameLevels[currentLevelIndex].StartLevel();
        }
    }

    public void NextLevel()
    {
        if (currentLevelIndex < gameLevels.Count - 1)
        {
            currentLevelIndex++;
            gameLevels[currentLevelIndex].StartLevel();
        }
        else
        {
            gameState = GameState.GameOver;
            currentLevelIndex = -1;
            RaiseOnGameOVer(EventArgs.Empty);   
            Debug.Log("Game Over! All levels completed.");
        }
    }

    public delegate void OnGameOver(object sender, EventArgs eventArgs);

    public event OnGameOver onGameOver;

    protected virtual void RaiseOnGameOVer(EventArgs e)
    {
        onGameOver?.Invoke(this, e);
    }

}
