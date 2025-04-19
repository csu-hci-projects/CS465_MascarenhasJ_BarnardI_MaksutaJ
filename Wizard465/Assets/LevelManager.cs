using System;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// Maximum time allowed for a level in seconds.
    /// </summary>
    public static int LEVEL_MAX_TIME = 600;
    /// <summary>
    /// Maximum number of tasks that can be completed in a level.
    /// </summary>
    public static int LEVEL_MAX_TASKS_COMPLETED = 5;

    public GameReference gameReference;
    private GameLevel currentLevel
    {
        get
        {
            GameLevel result = null;
            if (gameReference != null)
            {
                result = gameReference.currentLevel;
            }
            else
            {
                result = Game.Instance.currentLevel;
            }
            return result;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckAndEndLevelIfCompleted();
    }

    public void CheckAndEndLevelIfCompleted()
    {
        if (currentLevel != null)
        {
            bool isCompleted = IsLevelCompleted();

            if (isCompleted)
            {
                currentLevel.EndLevel();
                string levelReport = currentLevel.GetReportText();
                gameReference.Log(levelReport);
                gameReference.theGame.NextLevel();
            }
        }
    }

    public bool IsLevelCompleted()
    {
        return (TimeElapsedExceeded() || TasksCompleted());
    }

    private bool TimeElapsedExceeded()
    {
        bool result = false;

        if (currentLevel != null)
        {
            TimeSpan maxTime = TimeSpan.FromSeconds(LEVEL_MAX_TIME);
            DateTime elapsedTime = new DateTime(currentLevel.StartTime.Ticks + maxTime.Ticks);
            result = (DateTime.Now > elapsedTime);
        }

        return result;
    }

    public bool TasksCompleted()
    {
        bool result = false;
        if (currentLevel != null)
        {
            result = currentLevel.CompletedTasksCount >= LEVEL_MAX_TASKS_COMPLETED;
        }
        return result;
    }

}
