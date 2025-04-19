using UnityEngine;
using UnityEngine.Events;

public class GameReference : MonoBehaviour
{
    //public static GameReference Instance { get; private set; }

    public Game theGame { get { return Game.Instance; } }

    public GameLevel currentLevel
    {
        get
        {
            GameLevel result = null;
            if (theGame != null)
            {
                result = theGame.currentLevel;
            }
            return result;
        }
    }

    public TextWriter logger;

    public UnityEvent onGameOver;

    private void Awake()
    {
        //if (Instance == null)
        //{
        //    Instance = this;
        //    // Keep this object across scene loads
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theGame.onGameOver += (sender, eventArgs) =>
        {
            if (onGameOver != null)
            {
                onGameOver.Invoke(); // Invoke the UnityEvent with the recognized text
            }
            else
            {
                Debug.Log("Game Over! All levels completed.");
                Application.Quit();
            }

        };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Log(string message)
    {
        if (logger != null)
        {
            logger.AppendAllText(message);
        }
        else
        {
            Debug.Log(message);
        }
    }
}
