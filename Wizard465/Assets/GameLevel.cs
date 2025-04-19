using Assets;
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
    private int _completedTasksCount;

    public int LatinSquareValue
    {
        get { return _latinSquareValue; }
        set { _latinSquareValue = value; }
    }
    public InputMethod InputMethod
    {
        get { return _inputMethod; }
        set { _inputMethod = value; }
    }

    public GameLevelState LevelState
    {
        get { return _levelState; }
        set { _levelState = value; }
    }

    public DateTime StartTime
    {
        get { return _startTime; }
        set { _startTime = value; }
    }

    public DateTime EndTime
    {
        get { return _endTime; }
        set { _endTime = value; }
    }
    public int CompletedTasksCount
    {
        get { return _completedTasksCount; }
        set { _completedTasksCount = value; }
    }

    public TaskLog taskLog;

    public GameLevel()
    {
        this.LatinSquareValue = 0;
        this.InputMethod = InputMethod.None;
        this.LevelState = GameLevelState.Ready;
        this.StartTime = DateTime.MinValue;
        this.EndTime = DateTime.MinValue;
        this.taskLog = new TaskLog();
        this.CompletedTasksCount = 0;
    }

    public GameLevel(int latinSquareValue, InputMethod inputMethod) : this()
    {
        this.LatinSquareValue = latinSquareValue;
        this.InputMethod = inputMethod;
    }

    public void StartLevel()
    {
        StartTime = DateTime.Now;
        LevelState = GameLevelState.Playing;

        ReloadScene();
    }

    public void EndLevel()
    {
        EndTime = DateTime.Now;
        LevelState = GameLevelState.Finished;
    }

    private void ReloadScene()
    {
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex);
    }

    public bool isCompleted
    {
        get
        {
            return LevelState == GameLevelState.Finished;
        }
    }

    public int TasksCompleted
    {
        get
        {
            return (CompletedTasksCount / LevelManager.LEVEL_MAX_TASKS_COMPLETED);
        }
    }

    public double LevelDuration
    {
        get
        {
            return (EndTime - StartTime).TotalSeconds;
        }
    }

    public bool TimeExceeded
    {
        get
        {
            return (LevelDuration > LevelManager.LEVEL_MAX_TIME);
        }
    }

    public bool LevelSuccess
    {
        get
        {
            return (CompletedTasksCount >= LevelManager.LEVEL_MAX_TASKS_COMPLETED && LevelDuration <= LevelManager.LEVEL_MAX_TIME);
        }
    }

    public bool LevelFailed
    {
        get
        {
            return (CompletedTasksCount < LevelManager.LEVEL_MAX_TASKS_COMPLETED || TimeExceeded);
        }
    }

    public string GetReportText()
    {
        String result = String.Empty;
        result += $"---------------------\n";
        result += $"Level End:\n";
        result += $"---------------------\n";
        result += $"Level State: {LevelState}\n";
        result += $"Latin Square Value: {LatinSquareValue}\n";
        result += $"Input Method: {InputMethod}\n";
        result += $"Start Time: {StartTime}\n";
        result += $"End Time: {EndTime}\n";
        result += $"Completed Tasks Count: {CompletedTasksCount}\n";
        result += $"Duration: {LevelDuration} seconds\n";
        result += $"Task Log:\n";
        result += $"---------------------\n";
        result += this.taskLog.ToString() + "\n";   
        result += $"---------------------\n";
        result += $"Tasks Completed: {TasksCompleted}\n";
        result += $"Level Duration: {Math.Round(LevelDuration, 2)} seconds\n";
        result += $"Level Max Time: {LevelManager.LEVEL_MAX_TIME} seconds\n";
        result += $"Level Completed: {isCompleted}\n";
        result += $"Level Time Exceeded: {TimeExceeded}\n";
        result += $"Level Success: {LevelSuccess}\n";
        result += $"Level Failed: {LevelFailed}\n";
        result += $"---------------------\n\n";

        return result;
    }

}