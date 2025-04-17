using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class HandVisibilityToggle : MonoBehaviour
{
    [SerializeField] private NearFarInteractor handInteractor;

    private SkinnedMeshRenderer handModel;
    private bool isGrabbed = false;

    private void Start()
    {
        handModel = GetComponentInChildren<SkinnedMeshRenderer>();
        handInteractor.selectEntered.AddListener(OnGrab);
        handInteractor.selectExited.AddListener(OnLetGo);
    }

    private void Update()
    {
        if (isGrabbed)
        {
            if (handInteractor.selectionRegion.Value == NearFarInteractor.Region.Near)
            {
                if (handModel.enabled)
                {
                    handModel.enabled = false;
                }
            }
        }
        else
        {
            if (!handModel.enabled)
            {
                handModel.enabled = true;
            }
        }
    }

    private void OnLetGo(SelectExitEventArgs arg0)
    {
        isGrabbed = false;
    }

    private void OnGrab(SelectEnterEventArgs arg0)
    {
        isGrabbed = true;
    }
}
