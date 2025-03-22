using UnityEngine;
using System.Collections.Generic;
using Unity.XR.CoreUtils.Datums;
using System;
using Assets;
using UnityEngine.InputSystem.LowLevel;
public class SymbolTracer : MonoBehaviour
{
    public List<Vector3> tracedPathSolo = new List<Vector3>();
    public List<Vector3> tracedPathLeft = new List<Vector3>();
    public List<Vector3> tracedPathRight = new List<Vector3>();

    public float samplingInterval = 0.01f;
    private float timeSinceLastSample = 0f;
    public bool isRecording = false;
    public InputPositionSource inputSource;
    public DebugTextDisplay debugTextDisplay;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tracedPathSolo = new List<Vector3>();
        tracedPathLeft = new List<Vector3>();
        tracedPathRight = new List<Vector3>();
        samplingInterval = 0.01f;
        timeSinceLastSample = 0f;
        isRecording = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRecording && inputSource != null)
        {
            timeSinceLastSample += Time.deltaTime;
            if (timeSinceLastSample >= samplingInterval)
            {
                timeSinceLastSample = 0f;
                if (inputSource.GetNumberOfInputs() > 1)
                {
                    Vector3 leftPosition = inputSource.GetInputPosition()[0];
                    Vector3 rightPosition = inputSource.GetInputPosition()[1];
                    tracedPathLeft.Add(leftPosition);
                    tracedPathRight.Add(rightPosition);

                }
                else if (inputSource.GetNumberOfInputs() == 1)
                {
                    Vector3 currentPosition = GetInputPosition();
                    tracedPathSolo.Add(currentPosition);
                }
                CleanupPaths(); 
            }
            
            ShowDebugText();
        }
    }

    private Vector3 GetInputPosition()
    {
        if (inputSource == null)
        {
            Debug.LogError("Input source is not set.");
            return Vector3.zero;
        }
        return inputSource.GetInputPosition()[0];
    }

    public void StartRecording()
    {
        tracedPathSolo.Clear();
        tracedPathLeft.Clear();
        tracedPathRight.Clear();
        isRecording = true;
    }

    public void StopRecording()
    {
        isRecording = false;
    }

    private int NumberOfInputs()
    {
        int result = 1;
        if (inputSource != null)
        {
            result = inputSource.GetNumberOfInputs();
        }
        else
        {
            Debug.LogError("Input source is not set.");
        }
        return result;
    }

    private void CleanupPaths()
    {
        if (inputSource.GetNumberOfInputs() > 1)
        {
            if (tracedPathLeft.Count > 5)
            {
                tracedPathLeft.RemoveRange(0, tracedPathLeft.Count - 5);    
            }
            if (tracedPathRight.Count > 5)
            {
                tracedPathRight.RemoveRange(0, tracedPathRight.Count - 5);
            }

        }
        else if (inputSource.GetNumberOfInputs() == 1)
        {
            if (tracedPathSolo.Count > 5)
            {
                tracedPathSolo.RemoveRange(0, tracedPathSolo.Count - 5);
            }
        }
    }

    private void ShowDebugText()
    {
        string debugText = "";
        if (NumberOfInputs() > 1)
        {
            debugText = "Traced Path\nLeft\t Right:\n";

            int rows = Math.Max(tracedPathLeft.Count, tracedPathRight.Count);

            for (int n = 0; n < rows; n++)
            {
                String left = "";
                String right = "";
                if (n < tracedPathLeft.Count)
                {
                    left = tracedPathLeft[n].ToString("F2");
                }
                else
                {
                    left = "N/A";
                }
                if (n < tracedPathRight.Count)
                {
                    right = tracedPathRight[n].ToString("F2");
                }
                else
                {
                    right = "N/A";
                }
                String line = left + "\t" + right;
                debugText += line + "\n";
            }


        }
        else if (NumberOfInputs() == 1)
        {
            debugText = "Traced Path:\n";
            foreach (Vector3 point in tracedPathSolo)
            {
                debugText += point.ToString("F2") + "\n";
            }
        }

        ShowDebugText(debugText);
    }

    private void ShowDebugText(string text)
    {
        if (debugTextDisplay != null)
        {

            debugTextDisplay.UpdateDebugText(text);
        }
    }
}
