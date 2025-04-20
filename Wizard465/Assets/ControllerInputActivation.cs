using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ControllerInputActivation : MonoBehaviour
{

    public InputActionReference primaryButtonLeftReference;
    public InputActionReference primaryButtonRightReference;
    public InputActionReference triggerLeftReference;
    public InputActionReference triggerRightReference;

    private InputAction primaryButtonLeftAction;
    private InputAction primaryButtonRightAction;
    private InputAction triggerLeftAction;
    private InputAction triggerRightAction;

    public UnityEvent onActivate;

    public UnityEvent onDeactivate;

    void Awake()
    {
        primaryButtonLeftAction = primaryButtonLeftReference.action;
        primaryButtonRightAction = primaryButtonRightReference.action;
        triggerLeftAction = triggerLeftReference.action;
        triggerRightAction = triggerRightReference.action;
    }

    void OnEnable()
    {
        primaryButtonLeftAction.Enable();
        primaryButtonRightAction.Enable();
        triggerLeftAction.Enable();
        triggerRightAction.Enable();
    }

    void OnDisable()
    {
        primaryButtonLeftAction.Disable();
        primaryButtonRightAction.Disable();
        triggerLeftAction.Disable();
        triggerRightAction.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Update()
    {
        CheckButtonPressed(primaryButtonLeftAction);
        CheckButtonPressed(primaryButtonRightAction);
        //// Check if the primary button on the left controller is pressed
        //if (primaryButtonLeftAction.IsPressed())
        //{
        //    Debug.Log("Left Primary Button Pressed");
        //    onActivate.Invoke();
        //}

        //if (primaryButtonLeftAction.WasReleasedThisFrame())
        //{
        //    Debug.Log("Left Primary Button Pressed");
        //    onDeactivate.Invoke();
        //}

        //// Check for the first press event of the primary button on the right controller
        //if (primaryButtonRightAction.WasPressedThisFrame())
        //{
        //    Debug.Log("Right Primary Button Just Pressed");
        //}

        //// Check for the first release event of the trigger on the left controller
        //if (triggerLeftAction.WasReleasedThisFrame())
        //{
        //    Debug.Log("Left Trigger Just Released");
        //}

        //// Get the current value of the right trigger (e.g., for analog triggers)
        //float rightTriggerValue = triggerRightAction.ReadValue<float>();
        //if (rightTriggerValue > 0.1f) // Example threshold
        //{
        //    Debug.Log($"Right Trigger Value: {rightTriggerValue}");
        //}
    }

    private void CheckButtonPressed(InputAction inputAction)
    {
        if (inputAction.IsPressed())
        {
            Debug.Log("Left Primary Button Pressed");
            onActivate.Invoke();
        }

        if (inputAction.WasReleasedThisFrame())
        {
            Debug.Log("Left Primary Button Pressed");
            onDeactivate.Invoke();
        }
    }
}
