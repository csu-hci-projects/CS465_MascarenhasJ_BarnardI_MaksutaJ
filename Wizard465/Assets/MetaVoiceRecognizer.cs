using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class MetaVoiceRecognizer : MonoBehaviour, ISpeechRecognizer
    {
        public void Start()
        {
            
        }

        public void Update()
        {

        }
        
        Task<string> ISpeechRecognizer.RecognizeSpeech()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            Debug.Log("Meta voice recognition");
            // TODO: add meta voice code here.
            return "Meta Voice Result";
#else
            return null;
#endif
        }
    }
}
