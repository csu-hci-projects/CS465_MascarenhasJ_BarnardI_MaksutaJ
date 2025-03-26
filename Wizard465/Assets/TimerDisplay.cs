using TMPro;
using UnityEngine;

namespace Assets
{
    public class TimerDisplay : MonoBehaviour
    {
        public TimerController timer;

        private TextMeshProUGUI _uiTextPro;

        // Start is called before the first frame update
        void Start()
        {
            _uiTextPro = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            // Note: This is not optimized and you should avoid updating time each frame.
            _uiTextPro.text = timer.GetFormattedTimeFromSeconds();
        }
    }

}
