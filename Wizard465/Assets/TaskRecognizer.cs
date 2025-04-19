using Assets;
using System;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class TaskRecognizer : MonoBehaviour
{
    public const int TASK_WATCH_TIMEOUT_SECS = 30; // Timeout in seconds for task watch, if needed to reset the watch time.

    public TextWriter logWriter; // Reference to the TextWriter component, if needed for logging
    public TaskLog taskLog; // Reference to the TaskLog component, if needed for logging task data
    public VoiceRecognizer voiceRecognizer; // Reference to the VoiceRecognizer component
    public GestureRecognizer gestureRecognizer;

    public List<EventPairs> eventPairs = new List<EventPairs>();

    private DateTime watchTime = DateTime.MinValue;

    private TaskData currentTask;

    [Serializable]
    public class EventPairs
    {
        public string keyword;
        public UnityEvent<string[]> onSuccess;
        public UnityEvent<string[]> onFailure;

        public EventPairs(string keyword, UnityEvent<string[]> onSuccess, UnityEvent<string[]> onFailure)
        {
            this.keyword = keyword;
            this.onSuccess = onSuccess;
            this.onFailure = onFailure;
        }
    }

    public void OnTaskRecognized(string recognizedText)
    {
        if (string.IsNullOrEmpty(recognizedText))
        {
            Debug.LogWarning("No recognized text provided.");
            return;
        }
        if (this.eventPairs == null)
        {
            Debug.LogWarning("eventPairs list is null. Ensure it is initialized properly before calling OnVoiceRecognized.");
            return;
        }
        foreach (EventPairs aPair in eventPairs)
        {
            if (aPair.keyword == recognizedText)
            {
                if (aPair.onSuccess != null)
                {
                    aPair.onSuccess.Invoke(new string[] { recognizedText }); // Invoke the UnityEvent with the recognized text
                }
            }
        }
        Debug.Log("OnTaskRecognized called with: " + recognizedText);
    }

    public void OnTaskFailure(string recognizedText, string recognizedGesture)
    {
        if (string.IsNullOrEmpty(recognizedText))
        {
            Debug.LogWarning("No recognized text provided.");
            return;
        }
        if (this.eventPairs == null)
        {
            Debug.LogWarning("eventPairs list is null. Ensure it is initialized properly before calling OnVoiceRecognized.");
            return;
        }
        foreach (EventPairs aPair in eventPairs)
        {
            if (aPair.keyword == recognizedText)
            {
                if (aPair.onFailure != null)
                {
                    aPair.onFailure.Invoke(new string[] { recognizedText }); // Invoke the UnityEvent with the recognized text
                }
            }
        }
        Debug.Log("OnTaskFailure called with: " + recognizedText);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool voiceRecognized = false;
        bool gestureRecognized = false;

        DateTime currentTime = DateTime.Now;

        if (voiceRecognizer != null)
        {
            voiceRecognized = voiceRecognizer.IsPhraseRecognized();
        }
        if (gestureRecognizer != null)
        {
            gestureRecognized = gestureRecognizer.IsGestureRecognized();
        }
        if (voiceRecognized || gestureRecognized)
        {
            CreateTaskDataIfNull(currentTime);
        }
        DateTime timeLimit = watchTime.Add(new TimeSpan(0, 0, TASK_WATCH_TIMEOUT_SECS));
        // Check if the voice recognizer has recognized a phrase
        if (voiceRecognized && gestureRecognized)
        {
            string recognizedPhrase = voiceRecognizer.GetRecognizedPhrase();
            string recognizedGesture = gestureRecognizer.GetRecognizedGesture();

            if (recognizedPhrase.ToLower() == recognizedGesture.ToLower())
            {
                UpdateTaskData(currentTime);
                // Perform the task based on the recognized phrase and gesture
                OnTaskRecognized(recognizedPhrase);

                RaiseOnSuccess(new TaskRecognizedEventArgs(recognizedPhrase, recognizedGesture));
                TimeSpan elapsedTimeSeconds = DateTime.Now - watchTime;

                logData(string.Format("Task recognized successfully with: {0}. elapsed time (s): {1}", recognizedPhrase, elapsedTimeSeconds));
                cleanupAfterEvent();
            }
            else
            {
                UpdateTaskData(currentTime);
                OnTaskFailure(recognizedPhrase, recognizedGesture);
                RaiseOnFailure(new TaskRecognizedEventArgs(recognizedPhrase, recognizedGesture));
                logData(string.Format("Task recognition failed. Voice: {0}, Gesture: {1}. Timeout after {2} seconds.",
                    recognizedPhrase ?? "none",
                    recognizedGesture ?? "none",
                    TASK_WATCH_TIMEOUT_SECS));
                cleanupAfterEvent();
            }
        }
        else if ((voiceRecognized || gestureRecognized) && DateTime.Now > timeLimit)
        {
            string recognizedPhrase = voiceRecognizer.GetRecognizedPhrase();
            string recognizedGesture = gestureRecognizer.GetRecognizedGesture();

            OnTaskFailure(recognizedPhrase, recognizedGesture);
            RaiseOnFailure(new TaskRecognizedEventArgs(recognizedPhrase, recognizedGesture));
            logData(string.Format("Task recognition failed. Voice: {0}, Gesture: {1}. Timeout after {2} seconds.",
                recognizedPhrase ?? "none",
                recognizedGesture ?? "none",
                TASK_WATCH_TIMEOUT_SECS));
            cleanupAfterEvent();
        }
    }

    private void UpdateTaskData(DateTime currentTime)
    {
        if (this.voiceRecognizer != null)
        {
            this.currentTask.recognizedPhrase = this.voiceRecognizer.GetRecognizedPhrase();
            this.currentTask.recognizedPhraseTime = this.voiceRecognizer.RecognizedTime;    
        }
        if (this.gestureRecognizer != null)
        {
            this.currentTask.recognizedGesture = this.gestureRecognizer.GetRecognizedGesture();
        }
    }

    private void CreateTaskDataIfNull(DateTime currentTime)
    {
        if (this.currentTask == null)
        {
            this.currentTask = new TaskData();
            this.currentTask.currentTime = currentTime;
        }
    }

    private void logData(string message)
    {
        if (logWriter == null)
        {
            Debug.LogWarning("logWriter is not set. Cannot log data.");
            return;
        }
        logWriter.LogData(DateTime.Now, "TaskRecognizer", message);
    }

    private void LogTask(TaskData taskData)
    {
        try
        {
            if (taskLog != null)
            {
                taskLog.LogTask(taskData);
            }
            else
            {
                Debug.LogWarning("taskLog is not set. Cannot log task data.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message, this);
        }
    }

    private void cleanupAfterEvent()
    {
        this.watchTime = DateTime.MinValue; // Reset the watch time after the event is processed    
        this.voiceRecognizer.Reset();
        this.gestureRecognizer.Reset(); // Reset the gesture recognizer as well
        this.currentTask = null; // Reset the current task data
    }

    internal void OnVoiceRecognized(string recognizedText)
    {
        throw new NotImplementedException();
    }


    public delegate void OnSuccess(object sender, TaskRecognizedEventArgs eventArgs);

    public event OnSuccess onSuccess;

    protected virtual void RaiseOnSuccess(TaskRecognizedEventArgs e)
    {
        onSuccess?.Invoke(this, e);
    }

    public delegate void OnFailure(object sender, TaskRecognizedEventArgs eventArgs);

    public event OnFailure onFailure;

    protected virtual void RaiseOnFailure(TaskRecognizedEventArgs e)
    {
        onFailure?.Invoke(this, e);
    }


    public class TaskRecognizedEventArgs : EventArgs
    {
        public string RecognizedVoice { get; private set; }
        public string RecognizedGesture { get; private set; }
        public TaskRecognizedEventArgs(string recognizedVoice, string recognizedGesture) : base()
        {
            this.RecognizedVoice = recognizedVoice;
            this.RecognizedGesture = recognizedGesture;
        }
    }

}
