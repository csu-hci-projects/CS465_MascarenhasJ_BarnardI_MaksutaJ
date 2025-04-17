using System;
using static Game;

public class GameLevel
{
    public enum GameLevelState
    {
        Ready,
        Playing,
        Finished
    }

    private int _latinSquareValue;
    private InputMethod _inputMethod;
    private GameLevelState _levelState;
    private DateTime _startTime;
    private DateTime _endTime;

    public int latinSquareValue
    {
        get { return _latinSquareValue; }
        set { _latinSquareValue = value; }
    }
    public InputMethod inputMethod
    {
        get { return _inputMethod; }
        set { _inputMethod = value; }
    }

    public GameLevelState levelState
    {
        get { return _levelState; }
        set { _levelState = value; }
    }

    public DateTime startTime
    {
        get { return _startTime; }
        set { _startTime = value; }
    }

    public DateTime endTime
    {
        get { return _endTime; }
        set { _endTime = value; }
    }

    public GameLevel(int latinSquareValue, InputMethod inputMethod)
    {
        this.latinSquareValue = latinSquareValue;
        this.inputMethod = inputMethod;
        this.levelState = GameLevelState.Ready;
    }

    public void StartLevel()
    {
        startTime = DateTime.Now;
        levelState = GameLevelState.Playing;

        ReloadScene();
    }   

    public void EndLevel()
    {
        endTime = DateTime.Now;
        levelState = GameLevelState.Finished;
    }

    private void ReloadScene()
    {
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex);
    }

}