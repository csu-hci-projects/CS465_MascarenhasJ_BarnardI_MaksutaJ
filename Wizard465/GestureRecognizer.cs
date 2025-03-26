using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assembly_CSharp
{
    public class GestureRecognizer : MonoBehaviour
    {
        private KeyValuePair<string, System.Action> recognizedGesture;

        public void Start()
        {
            
        }

        public void Update()
        {
            // Check if a gesture is recognized
            if (IsGestureRecognized())
            {
                // Perform the action associated with the recognized gesture
                recognizedGesture.Value.Invoke();
            }
        }

        internal bool IsGestureRecognized()
        {
            return (true);
        }

        internal KeyValuePair<string, System.Action> GetRecognizedGesture()
        {
            return recognizedGesture;
        }
    }
}
