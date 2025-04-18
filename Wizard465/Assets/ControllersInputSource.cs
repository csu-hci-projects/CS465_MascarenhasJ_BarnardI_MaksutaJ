using Assets;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllersInputSource : InputPositionSource
{
    public Vector3 _LeftPosition;
    public Vector3 _RightPosition;

    private Quaternion _LeftRotation;
    private Quaternion _RightRotation;

    public Vector3 LeftPosition { get { return _LeftPosition; } set { _LeftPosition = value; } }
    public Vector3 RightPosition { get { return _RightPosition; } set { _RightPosition = value; } }

    public Quaternion LeftRotation { get { return _LeftRotation; } set { _LeftRotation = value; } }
    public Quaternion RightRotation { get { return _RightRotation; } set { _RightRotation = value; } }

    public InputActionProperty leftControllerPositionActionProperty;
    public InputActionProperty leftControllerRotationActionProperty;
    public InputActionProperty rightControllerPositionActionProperty;
    public InputActionProperty rightControllerRotationActionProperty;

    private InputAction leftControllerPositionAction;
    private InputAction leftControllerRotationAction;
    private InputAction rightControllerPositionAction;
    private InputAction rightControllerRotationAction;

    public override Vector3[] GetInputPosition()
    {
        return new Vector3[] { this.LeftPosition, this.RightPosition };
    }

    public override int GetNumberOfInputs()
    {
        return 2;
    }

    void Start()
    {

    }

    public void Awake()
    {
        leftControllerPositionAction = leftControllerPositionActionProperty.action;
        leftControllerRotationAction = leftControllerRotationActionProperty.action;
        rightControllerPositionAction = rightControllerPositionActionProperty.action;
        rightControllerRotationAction = rightControllerRotationActionProperty.action;
    }

    public void Update()
    {
        LeftPosition = leftControllerPositionAction.ReadValue<Vector3>();
        LeftRotation = leftControllerRotationAction.ReadValue<Quaternion>();
        RightPosition = rightControllerPositionAction.ReadValue<Vector3>();
        RightRotation = rightControllerRotationAction.ReadValue<Quaternion>();
    }

    public void OnEnable()
    {
        leftControllerPositionAction.Enable();
        leftControllerRotationAction.Enable();
        rightControllerPositionAction.Enable();
        rightControllerRotationAction.Enable();

    }

    public void OnDisable()
    {
        leftControllerPositionAction.Disable();
        leftControllerRotationAction.Disable();
        rightControllerPositionAction.Disable();
        rightControllerRotationAction.Disable();
    }

}
