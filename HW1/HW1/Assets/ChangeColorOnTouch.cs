using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChangeColorOnTouch : MonoBehaviour
{
    private Renderer blockRenderer;
    private Color originalColor;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    void Start()
    {
        blockRenderer = GetComponent<Renderer>();
        originalColor = GetComponent<Renderer>().material.color;

        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();

        if (interactable != null)
        
        {
            interactable.hoverEntered.AddListener(ChangeToWhite);
            interactable.hoverExited.AddListener(ResetColor);
        }
    }

    private void ChangeToWhite(HoverEnterEventArgs args)
    {
        blockRenderer.material.color = Color.white;
    }

    private void ResetColor(HoverExitEventArgs args)
    {
        blockRenderer.material.color = originalColor;
    }
}
