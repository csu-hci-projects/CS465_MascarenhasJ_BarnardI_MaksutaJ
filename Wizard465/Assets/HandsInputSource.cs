using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.OpenXR.Input;

namespace Assets
{
    public class HandsInputSource : InputPositionSource
    {
        public InputDevice leftHand;
        public InputDevice rightHand;
        public Vector3 leftHandPosition;
        public Vector3 rightHandPosition;   

        public override Vector3[] GetInputPosition()
        {
            return new Vector3[] { leftHandPosition, rightHandPosition };
        }

        public override int GetNumberOfInputs()
        {
            return 2;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created 
        public void Start()
        {
            leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        }

        public void Update()
        {
            UpdateHandPositions();
        }

        void UpdateHandPositions()
        {
            if (leftHand.isValid)
            {
                leftHand.TryGetFeatureValue(CommonUsages.devicePosition, out leftHandPosition);
            }
            else
            {
                leftHandPosition = Vector3.zero;
            }
            if (rightHand.isValid)
            {
                rightHand.TryGetFeatureValue(CommonUsages.devicePosition, out rightHandPosition);
            }
            else
            {
                rightHandPosition = Vector3.zero;
            }
        }
    }
}
