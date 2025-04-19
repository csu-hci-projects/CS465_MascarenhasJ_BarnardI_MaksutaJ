using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static Game;

namespace Assets
{
    public class InputSourceManager : MonoBehaviour
    {
        private static InputSourceManager instance;

        private static InputSourceManager getInstance()
        {
            if (instance == null)
            {
                instance = new InputSourceManager();
            }
            return instance;
        }


        public List<InputMethodSource> inputSources = new List<InputMethodSource>();

        public InputSourceManager()
        {
            instance = this;
            inputSources = new List<InputMethodSource>();
        }

        //public void InstanceSourceManager()
        //{
        //    //instance = this;
        //}

        [Serializable]
        public class InputMethodSource
        {
            public Game.InputMethod inputMethod;
            public InputPositionSource inputPositionSource;

            //public UnityEvent<string[]> onSuccess;
            //public UnityEvent<string[]> onFailure;

            public InputMethodSource(Game.InputMethod inputMethod, InputPositionSource inputPositionSource)
            {
                this.inputMethod = inputMethod;
                this.inputPositionSource = inputPositionSource;
            }
        }

        public void Start()
        {
            UpdateInputSources();
        }

        public void Update()
        {
            UpdateInputSources();
        }

        private void UpdateInputSources()
        {
            Game game = Game.Instance;

            int latinSquareValue = game.CurrentLatinSquareValue();
            InputMethod currentInputMethod = game.inputMethod;

            if (this.inputSources != null)
            {
                foreach (InputMethodSource source in this.inputSources)
                {
                    source.inputPositionSource.enabled = (source.inputMethod == currentInputMethod);
                }
            }
        }

        public static InputPositionSource GetCurrentInputSource()
        {
            InputPositionSource result = null;
            Game game = Game.Instance;

            int latinSquareValue = game.CurrentLatinSquareValue();
            InputMethod currentInputMethod = game.inputMethod;

            InputSourceManager manager = getInstance();

            if (manager.inputSources != null)
            {
                foreach (InputMethodSource source in manager.inputSources)
                {
                    if (source.inputMethod == currentInputMethod)
                    {
                        result = source.inputPositionSource;
                        break;
                    }
                }
            }
            return result;
        }

    }
}
