using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    public class TaskLog
    {
        private List<TaskData> taskDataList;    

        public TaskLog()
        {
            taskDataList = new List<TaskData>();
        }

        public void LogTask(TaskData taskData)
        {
            if (taskData == null)
            {
                throw new ArgumentNullException(nameof(taskData), "TaskData cannot be null");
            }
            taskDataList.Add(taskData);
        }

        public override string ToString()
        {
            string result = $"TaskLog: {DateTime.Now}\n";
            result += $"Time:\t Phrase:\t Phrase Time:\t Gesture:\t Gesture Time:\t Task:\t Task Time:\t Success:\t Failure:\t\n";
            foreach (TaskData data in taskDataList)
            {
                result += $"{data.currentTime}\t{data.recognizedPhrase}\t{data.recognizedPhraseTime}\t{data.recognizedGesture}\t{data.recognizedGestureTime}\t{data.recognizedTask}\t{data.recognizedTaskTime}\t{data.isSuccess}\t{data.isFailure}\n";
            }
            return result;
        }
    }
}
