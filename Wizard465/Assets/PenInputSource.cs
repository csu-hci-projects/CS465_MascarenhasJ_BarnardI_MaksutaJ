using Assets;
using UnityEngine;
using UnityEngine.InputSystem;

public class PenInputSource : InputPositionSource  
{
    private MxInkHandler handler;
    protected StylusInputs _stylus;
    private InputActionReference _tipActionRef;
    private InputActionReference _grabActionRef;
    private InputActionReference _optionActionRef;
    private InputActionReference _middleActionRef;

    public Vector3 penPosition;
    public override Vector3[] GetInputPosition()
    {
        return new Vector3[] { penPosition };
    }
    public override int GetNumberOfInputs()
    {
        return 1;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        // Initialize pen position if needed
        penPosition = Vector3.zero;
        handler = new MxInkHandler();
        _stylus = handler.CurrentState;

        _tipActionRef.action.Enable();
        _grabActionRef.action.Enable();
        _optionActionRef.action.Enable();
        _middleActionRef.action.Enable();
    }
    public void Update()
    {
        // Update pen position based on input
        // This is a placeholder; actual implementation will depend on how the pen input is captured
        // For example, you might use a specific input device or API to get the current position of the pen
        _stylus = handler.CurrentState;
        if (_stylus.isActive)
        {
            _stylus.inkingPose.position = transform.position;
            _stylus.inkingPose.rotation = transform.rotation;
            _stylus.tip_value = _tipActionRef.action.ReadValue<float>();
            _stylus.cluster_middle_value = _middleActionRef.action.ReadValue<float>();
            _stylus.cluster_front_value = _grabActionRef.action.IsPressed();
            _stylus.cluster_back_value = _optionActionRef.action.IsPressed();

            penPosition = _stylus.inkingPose.position;
        }
        else
        {
            penPosition = Vector3.zero;
        }
    }

    public void Awake()
    {
        _tipActionRef.action.Enable();
        _grabActionRef.action.Enable();
        _optionActionRef.action.Enable();
        _middleActionRef.action.Enable();
    }
}
