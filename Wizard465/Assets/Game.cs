using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [Description("Controller")]
        Controller,
        [Description("Hands")]
        Hands,
        [Description("Pen")]
        Pen
    }

    public GameState gameState;
    public InputMethod inputMethod;
    public string latinSquareCode;

    public Game()
    {
        gameState = GameState.MainMenu;
        inputMethod = InputMethod.Controller;
        latinSquareCode = string.Empty;
    }   
}
