using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class VoiceRecognizerManager : MonoBehaviour
    {
        private ISpeechRecognizer speechRecognizer;

        void Start()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (SystemInfo.deviceModel.Contains("Quest"))
            {
                speechRecognizer = gameObject.AddComponent<MetaVoiceRecognizer>();
            }
            else
            {
                // TODO: Add Other Recognizer here.
            }
#else
            // TODO: Add Other Recognizer here.
#endif

            if (speechRecognizer == null)
            {
                Debug.LogError("No speech recognizer available.");
            }
        }

        public void Update()
        {

        }

        public async Task<string> RecognizeSpeech()
        {
            if (speechRecognizer != null)
            {
                return await speechRecognizer.RecognizeSpeech();
            }
            else
            {
                return null;
            }
        }
    }

}