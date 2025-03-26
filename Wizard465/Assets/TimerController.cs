using System;
using TMPro;
using UnityEngine;

namespace Assets
{
    /// <summary>
    /// Represents a countdown timer.
    /// </summary>
    public class TimerController : MonoBehaviour
    {
        private float _time = 0; // [sec] current time of the countdown timer.
        private bool _timerExist = false;
        private bool _timerRunning = false;

        public const int CONVERSION_MIN_TO_SEC = 60;
        public const int CONVERSION_HOUR_TO_SEC = 3600;

        [Tooltip("The UI text element to show app messages.")]
        public TextMeshProUGUI logTextPro;

        [Tooltip("The timer ring sound.")] public AudioClip buzzSound;

        // Update is called once per frame
        void Update()
        {
            if (_timerExist && _timerRunning)
            {
                _time -= Time.deltaTime;
                if (_time <= 0)
                {
                    // Raise a ring.
                    OnElapsedTime();
                }
            }
        }

        private void Log(string msg)
        {
            Debug.Log(msg);
            logTextPro.text = "Log: " + msg;
        }

        /// <summary>
        /// Buzzes and resets the timer.
        /// </summary>
        private void OnElapsedTime()
        {
            _time = 0;
            _timerRunning = false;
            _timerExist = false;
            Log("Buzz!");
            AudioSource.PlayClipAtPoint(buzzSound, Vector3.zero);
        }

        /// <summary>
        /// Deletes the timer. It corresponds to the wit intent "wit$delete_timer"
        /// </summary>
        public void DeleteTimer()
        {
            if (!_timerExist)
            {
                Log("Error: There is no timer to delete.");
                return;
            }

            _timerExist = false;
            _time = 0;
            _timerRunning = false;
            Log("Timer deleted.");
        }

        /// <summary>
        /// Creates a timer. It corresponds to the wit intent "wit$create_timer"
        /// </summary>
        /// <param name="entityValues">countdown in minutes.</param>
        public void CreateTimer(string[] entityValues)
        {
            if (_timerExist)
            {
                Log("A timer already exist.");
                return;
            }

            string duration = entityValues[0];
            string unit = entityValues[1]; // [sec, minute or hour].

            try
            {
                _time = getSeconds(duration, unit);
                _timerExist = true;
                _timerRunning = true;
                Log("Countdown Timer is set for " + duration + " " + unit + ".");
            }
            catch (Exception e)
            {
                Log("Error in CreateTimer(): Could not parse with reply.");
            }
        }

        /// <summary>
        /// Displays current timer value. It corresponds to "wit$get_timer".
        /// </summary>
        public void GetTimerIntent()
        {
            // Show the remaining time of the countdown timer.
            var msg = GetFormattedTimeFromSeconds();
            Log(msg);
        }

        /// <summary>
        /// Pauses the timer. It corresponds to the wit intent "wit$pause_timer"
        /// </summary>
        public void PauseTimer()
        {
            _timerRunning = false;
            Log("Timer paused.");
        }

        /// <summary>
        /// It corresponds to the wit intent "wit$resume_timer"
        /// </summary>
        public void ResumeTimer()
        {
            _timerRunning = true;
            Log("Timer resumed.");
        }

        /// <summary>
        /// Subtracts time from the timer. It corresponds to the wit intent "wit$subtract_time_timer".
        /// </summary>x
        /// <param name="entityValues"></param>
        public void SubtractTimeTimer(string[] entityValues)
        {
            if (!_timerExist)
            {
                Log("Error: No Timer is created.");
                return;
            }

            string duration = entityValues[0];
            string unit = entityValues[1];

            try
            {
                _time -= getSeconds(duration, unit);
                var msg = duration + " " + unit + " were subtracted from the timer.";
                Log(msg);
            }
            catch (Exception e)
            {
                Log("Error in SubtractTimeTimer(): Could not parse with reply.");
            }

            if (_time < 0)
            {
                _time = 0;
            }
        }

        /// <summary>
        /// Adds time to the timer. It corresponds to the wit intent "wit$add_time_timer".
        /// </summary>
        /// <param name="entityValues"></param>
        public void AddTimeToTimer(string[] entityValues)
        {
            string duration = entityValues[0];
            string unit = entityValues[1];

            if (!_timerExist)
            {
                Log("Timer does not exist. Creating a timer...");
                CreateTimer(entityValues);
                return;
            }

            try
            {
                _time += getSeconds(duration, unit);
                var msg = duration + " " + unit + "were added to the timer.";
                Log(msg);
            }
            catch (Exception e)
            {
                Log("Error in AddTimeToTimer(): Could not parse with reply.");
            }
        }

        /// <summary>
        /// Returns the remaining time (in sec) of the countdown timer.
        /// </summary>
        /// <returns></returns>
        public float GetRemainingTime()
        {
            return _time;
        }

        /// <summary>
        /// Returns the duration in seconds.
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private float getSeconds(string duration, string unit)
        {
            int factor = 1;
            if (unit == "second") factor = 1;
            if (unit == "minute") factor = CONVERSION_MIN_TO_SEC;
            if (unit == "hour") factor = CONVERSION_HOUR_TO_SEC;
            if (float.TryParse(duration, out float timeAmount))
            {
                return timeAmount * factor;
            }
            else
            {
                throw new ArgumentException("could not parse.");
            }
        }

        /// <summary>
        /// Returns time in the format of min:sec.
        /// </summary>
        /// <returns></returns>
        public string GetFormattedTimeFromSeconds()
        {
            return Mathf.FloorToInt(_time / 60.0f).ToString("0") + ":" + Mathf.FloorToInt(_time % 60.0f).ToString("00");
        }
    }
}
