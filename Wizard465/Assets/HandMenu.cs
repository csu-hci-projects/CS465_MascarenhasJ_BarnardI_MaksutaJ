using Unity.XR.CoreUtils.Datums;
using UnityEngine;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Management;

public class HandMenu : MonoBehaviour
{
    public XRHandSubsystem handSubsystem;
    public GameObject menuCanvas;
    public float pressThreshold = 0.05f; // Adjust this value to set the sensitivity of the press detection  
    public float holdDuration = 0.5f; // Duration to hold the button before showing the menu    

    private float holdStartTime = 0f;
    private bool isHolding = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        handSubsystem = XRGeneralSettings.Instance.Manager.activeLoader.GetLoadedSubsystem<XRHandSubsystem>();
        if (handSubsystem == null)
        {
            Debug.LogError("OpenXR Hand Subsystem is null.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (handSubsystem != null && handSubsystem.leftHand != null)
        {
            XRHandJoint indexTip = handSubsystem.leftHand.GetJoint(XRHandJointID.IndexTip); 
            XRHandJoint thumbTip = handSubsystem.leftHand.GetJoint(XRHandJointID.ThumbTip); 

            if (indexTip.TryGetPose(out Pose indexPose) && thumbTip.TryGetPose(out Pose thumbPose))
            {
                float distance = Vector3.Distance(indexPose.position, thumbPose.position);

                if (distance < pressThreshold)
                {
                    if (!isHolding)
                    {
                        if (holdStartTime == 0)
                        {
                            holdStartTime = Time.time;  
                        }
                        if (Time.time - holdStartTime >= holdDuration)
                        {
                            isHolding = true;
                            menuCanvas.SetActive(true); // Show the menu canvas
                        }   
                    }
                }
                else
                {
                    holdStartTime = 0;
                    if (isHolding)
                    {
                        isHolding = false;
                        menuCanvas.SetActive(false); // Hide the menu canvas    
                    }
                }
            }
        }
    }
}
