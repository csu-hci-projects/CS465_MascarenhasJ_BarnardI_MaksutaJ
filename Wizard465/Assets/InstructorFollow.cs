using UnityEngine;
using TMPro;

public class InstructorFollow : MonoBehaviour
{
    public GameObject vrCamera;
    public Transform instructingPosition;
    public Transform idlePosition;
    public TextMeshProUGUI instructionText;

    private bool isInstructing = false;
    private float switchDistance = 3f;

    void Update()
    {
        float distance = Vector3.Distance(vrCamera.transform.position, transform.position);
        
        if (distance < switchDistance)
        {
            if (!isInstructing)
            {
                isInstructing = true;
                MoveToInstructingPosition();
                DisplayInstruction("Welcome to the lesson!");
            }
        }
        else
        {
            if (isInstructing)
            {
                isInstructing = false;
                MoveToIdlePosition();
                DisplayInstruction("Take your time.");
            }
        }
    }

    private void MoveToInstructingPosition()
    {
        transform.position = instructingPosition.position;
        transform.rotation = instructingPosition.rotation;

        instructionText.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 2);
    }

    private void MoveToIdlePosition()
    {
        transform.position = idlePosition.position;
        transform.rotation = idlePosition.rotation;

        instructionText.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 2);
    }

    private void DisplayInstruction(string message)
    {
        if (instructionText != null)
        {
            instructionText.text = message;
        }
    }
}
