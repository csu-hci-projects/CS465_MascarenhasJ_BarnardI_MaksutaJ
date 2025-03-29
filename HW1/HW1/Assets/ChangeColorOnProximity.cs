using UnityEngine;
using UnityEngine.XR.Hands;

using UnityEngine;
using UnityEngine.XR;

public class HoverColorChange : MonoBehaviour
{
    public Material hoverMaterial;
    private Material originalMaterial; 
    private Renderer blockRenderer;

    void Start()
    {
        blockRenderer = GetComponent<Renderer>();
        originalMaterial = blockRenderer.material;
    }

    void OnEnter(Collider other)
    {
        if (other.CompareTag("Hand") || other.CompareTag("Controller"))
        {
            blockRenderer.material = hoverMaterial;
        }
    }

    void OnExit(Collider other)
    {
        if (other.CompareTag("Hand") || other.CompareTag("Controller"))
        {
            blockRenderer.material = originalMaterial;
        }
    }
}



