using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    public class TaskData
    {
        public DateTime currentTime;
        public string recognizedPhrase;
        public DateTime recognizedPhraseTime;
        public string recognizedGesture;
        public DateTime recognizedGestureTime;
        public string recognizedTask;
        public DateTime recognizedTaskTime;
        public bool isSuccess;
        public bool isFailure;

        public TaskData()
        {
            currentTime = DateTime.Now;
            recognizedPhrase = string.Empty;
            recognizedPhraseTime = DateTime.MinValue;
            recognizedGesture = string.Empty;
            recognizedGestureTime = DateTime.MinValue;
            recognizedTask = string.Empty;
            recognizedTaskTime = DateTime.MinValue;
            isSuccess = false;
            isFailure = false;
        }

        public TaskData(DateTime currentTime, string recognizedPhrase, DateTime recognizedPhraseTime, string recognizedGesture, DateTime recognizedGestureTime, string recognizedTask, DateTime recognizedTaskTime, bool isSuccess, bool isFailure)
        {
            this.currentTime = currentTime;
            this.recognizedPhrase = recognizedPhrase;
            this.recognizedPhraseTime = recognizedPhraseTime;
            this.recognizedGesture = recognizedGesture;
            this.recognizedGestureTime = recognizedGestureTime;
            this.recognizedTask = recognizedTask;
            this.recognizedTaskTime = recognizedTaskTime;
            this.isSuccess = isSuccess;
            this.isFailure = isFailure;
        }

    }
}
