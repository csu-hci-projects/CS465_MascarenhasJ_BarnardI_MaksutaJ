using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PenInputActivation : MonoBehaviour
{

    public InputActionReference penTipPressureReference;
    public InputActionReference penButtonReference;
    public InputActionReference penPositionReference;

    private InputAction penTipPressureAction;
    private InputAction penButtonAction;
    private InputAction penPositionAction;

    public UnityEvent onActivate;

    public UnityEvent onDeactivate;


    void Awake()
    {
        penTipPressureAction = penTipPressureReference.action;
        penButtonAction = penButtonReference.action;
        penPositionAction = penPositionReference.action;
    }

    void OnEnable()
    {
        penTipPressureAction.Enable();
        penButtonAction.Enable();
        penPositionAction.Enable();
    }

    void OnDisable()
    {
        penTipPressureAction.Disable();
        penButtonAction.Disable();
        penPositionAction.Disable();
    }

    void Start()
    {

    }

    void Update()
    {
        if (penButtonAction != null && penButtonAction.IsPressed())
        {
            Debug.Log("Pen Button Pressed");
            if (onActivate != null)
            {
                onActivate.Invoke();
            }
        }

        if (penButtonAction != null && penButtonAction.WasReleasedThisFrame())
        {
            Debug.Log("Pen Button Pressed");
            if (onDeactivate != null)
            {
                onDeactivate.Invoke();
            }
        }
    }

}
