using UnityEngine;

namespace Assets
{
    public abstract class InputPositionSource : MonoBehaviour
    {
        public abstract int GetNumberOfInputs();
        public abstract Vector3[] GetInputPosition();
    }
}
