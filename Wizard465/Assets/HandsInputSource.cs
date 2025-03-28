using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Management;
using UnityEngine.XR.OpenXR.Input;

namespace Assets
{
    public class HandsInputSource : InputPositionSource
    {
        public InputDevice leftHand;
        public InputDevice rightHand;
        public Vector3 leftHandPosition;
        public Vector3 rightHandPosition;

        public XRInputSubsystem xrInputSubsystem;
        public XRHandSubsystem xrHandSubsystem;

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
            xrInputSubsystem = XRGeneralSettings.Instance.Manager.activeLoader.GetLoadedSubsystem<XRInputSubsystem>();
            if (xrInputSubsystem == null)
            {
                Debug.LogError("OpenXR Input Subsystem is null.");
            }
            xrHandSubsystem = XRGeneralSettings.Instance.Manager.activeLoader.GetLoadedSubsystem<XRHandSubsystem>();
            if (xrHandSubsystem == null)
            {
                Debug.LogError("OpenXR Hand Subsystem is null.");
            }
            leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        }

        public void Update()
        {
            if (xrHandSubsystem != null && xrHandSubsystem.running)
            {
                var updateSuccessFlags = xrHandSubsystem.TryUpdateHands(XRHandSubsystem.UpdateType.Dynamic);

                if (xrHandSubsystem.leftHand.isTracked)
                {
                    leftHandPosition = xrHandSubsystem.leftHand.rootPose.position;
                }
                if (xrHandSubsystem.rightHand.isTracked)
                {
                    rightHandPosition = xrHandSubsystem.rightHand.rootPose.position;
                }
            }
            else
            {
                if (xrHandSubsystem != null)
                {
                    Debug.Log("Hand Subsystem is not running.");
                }
            }
            //UpdateHandPositions();
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
        //}
        //    }
        //    UpdateHandPositions();
        //}

        //void UpdateHandPositions()
        //{
        //    if (leftHand.isValid)
        //    {
        //        leftHand.TryGetFeatureValue(CommonUsages.devicePosition, out leftHandPosition);
        //    }
        //    else
        //    {
        //        leftHandPosition = Vector3.zero;
        //    }
        //    if (rightHand.isValid)
        //    {
        //        rightHand.TryGetFeatureValue(CommonUsages.devicePosition, out rightHandPosition);
        //    }
        //    else
        //    {
        //        rightHandPosition = Vector3.zero;
        //    }
        //}
    }
}
