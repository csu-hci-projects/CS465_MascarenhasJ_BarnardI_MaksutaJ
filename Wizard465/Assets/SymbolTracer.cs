using UnityEngine;
using System.Collections.Generic;
using Unity.XR.CoreUtils.Datums;
using System;
using Assets;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.Rendering.DebugUI.Table;
public class SymbolTracer : MonoBehaviour
{
    public List<Vector3> tracedPathSolo = new List<Vector3>();
    public List<Vector3> tracedPathLeft = new List<Vector3>();
    public List<Vector3> tracedPathRight = new List<Vector3>();

    private Vector3 lastLeftPosition = new Vector3();
    private Vector3 lastRightPosition = new Vector3();
    private Vector3 lastSoloPosition = new Vector3();

    public float samplingInterval = 0.01f;
    private float timeSinceLastSample = 0f;
    public bool isRecording = false;
    public InputPositionSource inputSource;
    public DebugTextDisplay debugTextDisplay;
    public VRVectorProjection vRVectorProjection;
    public Vector2ListToImage vector2ListToImage;

    public int MaxVectorListSize;
    public TMP_Text debugText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tracedPathSolo = new List<Vector3>();
        tracedPathLeft = new List<Vector3>();
        tracedPathRight = new List<Vector3>();
        lastLeftPosition = new Vector3();
        lastRightPosition = new Vector3();
        lastSoloPosition = new Vector3();
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

                    if (leftPosition != lastLeftPosition)
                    {
                        tracedPathLeft.Add(leftPosition);
                        lastLeftPosition = leftPosition; // Update the last left position for debugging or other purposes;
                    }
                    if (rightPosition != lastRightPosition)
                    {
                        tracedPathRight.Add(rightPosition);
                        lastRightPosition = rightPosition; // Update the last right position for debugging or other purposes;   
                    }

                }
                else if (inputSource.GetNumberOfInputs() == 1)
                {
                    Vector3 soloPosition = GetInputPosition();
                    if (soloPosition != lastSoloPosition)
                    {
                        tracedPathSolo.Add(soloPosition);
                        lastSoloPosition = soloPosition;
                    }
                }
                CleanupPaths();
            }
            CheckPath();
            ShowDebugText();
        }
    }

    private void CheckPath()
    {
        if (vRVectorProjection != null)
        {
            if (inputSource.GetNumberOfInputs() > 1)
            {
                List<Vector2> flatPathLeft =
                //vRVectorProjection.ProjectVector3PathToVector2(tracedPathLeft, vRVectorProjection.mainCamera);
                vRVectorProjection.ProjectVector3PathToViewport(tracedPathLeft);
                //vRVectorProjection.vector3PathToVector2ByPerspectiveDivide(tracedPathLeft);
                List<Vector2> flatPathRight =
                //vRVectorProjection.ProjectVector3PathToVector2(tracedPathRight, vRVectorProjection.mainCamera);
                vRVectorProjection.ProjectVector3PathToViewport(tracedPathRight);
                //vRVectorProjection.vector3PathToVector2ByPerspectiveDivide(tracedPathRight);


                if (vector2ListToImage != null)
                {
                    //vector2ListToImage.DisplayVector2List(flatPathLeft);
                    vector2ListToImage.DisplayVector2List(flatPathLeft, flatPathRight);
                    //vector2ListToImage.DrawVector2List(flatPathLeft);
                    //vector2ListToImage.DrawVector2List(flatPathRight);
                }
            }
            else if (inputSource.GetNumberOfInputs() == 1)
            {

                List<Vector2> flatPathSolo = vRVectorProjection.ProjectVector3PathToVector2(tracedPathSolo, vRVectorProjection.mainCamera);

                if (vector2ListToImage != null)
                {
                    vector2ListToImage.DrawVector2List(flatPathSolo);
                }
            }

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
            if (tracedPathLeft.Count > MaxVectorListSize)
            {
                tracedPathLeft.RemoveRange(0, tracedPathLeft.Count - MaxVectorListSize);
            }
            if (tracedPathRight.Count > MaxVectorListSize)
            {
                tracedPathRight.RemoveRange(0, tracedPathRight.Count - MaxVectorListSize);
            }

        }
        else if (inputSource.GetNumberOfInputs() == 1)
        {
            if (tracedPathSolo.Count > MaxVectorListSize)
            {
                tracedPathSolo.RemoveRange(0, tracedPathSolo.Count - MaxVectorListSize);
            }
        }
    }

    private void ShowDebugText()
    {
        string debugText = "";
        if (NumberOfInputs() > 1)
        {
            List<Vector2> flatPathLeft =
                //vRVectorProjection.ProjectVector3PathToVector2(tracedPathLeft, vRVectorProjection.mainCamera);
                vRVectorProjection.ProjectVector3PathToViewport(tracedPathLeft);
            //vRVectorProjection.vector3PathToVector2ByPerspectiveDivide(tracedPathLeft);
            List<Vector2> flatPathRight =
            //vRVectorProjection.ProjectVector3PathToVector2(tracedPathRight, vRVectorProjection.mainCamera);
            vRVectorProjection.ProjectVector3PathToViewport(tracedPathRight);
            //vRVectorProjection.vector3PathToVector2ByPerspectiveDivide(tracedPathRight);

            debugText = "Traced Path\nLeft\t Right:\n";
            debugText += getDebugOutput(tracedPathLeft, tracedPathRight);
            debugText += "Traced Path (2-D)\nLeft\t Right:\n";
            debugText += getDebugOutput(flatPathLeft, flatPathRight);
        }
        else if (NumberOfInputs() == 1)
        {
            List<Vector2> flatPathSolo = vRVectorProjection.ProjectVector3PathToVector2(tracedPathSolo, vRVectorProjection.mainCamera);

            debugText = "Traced Path:\n";
            debugText += getDebugOutput(tracedPathSolo) + "\n"; // Original 3D path for reference   
            debugText += "Traced Path (2-d):\n";
            debugText += getDebugOutput(flatPathSolo) + "\n";
        }

        ShowDebugText(debugText);
    }

    private String getDebugOutput(List<Vector3> left, List<Vector3> right)
    {
        String debugText = "";
        int rows = Math.Max(left.Count, right.Count);

        for (int n = 0; n < rows; n++)
        {
            String leftOut = "";
            String rightOut = "";
            if (n < left.Count)
            {
                leftOut = left[n].ToString("F2");
            }
            else
            {
                leftOut = "N/A";
            }
            if (n < right.Count)
            {
                rightOut = right[n].ToString("F2");
            }
            else
            {
                rightOut = "N/A";
            }
            debugText += leftOut + "\t" + rightOut + "\n";
        }
        return debugText;
    }

    private String getDebugOutput(List<Vector3> left)
    {
        String debugText = "";
        int rows = left.Count;

        for (int n = 0; n < rows; n++)
        {
            String leftOut = "";
            if (n < left.Count)
            {
                leftOut = left[n].ToString("F2");
            }
            else
            {
                leftOut = "N/A";
            }
            debugText += leftOut + "\n";
        }
        return debugText;
    }

    private String getDebugOutput(List<Vector2> left, List<Vector2> right)
    {
        String debugText = "";
        int rows = Math.Max(left.Count, right.Count);

        for (int n = 0; n < rows; n++)
        {
            String leftOut = "";
            String rightOut = "";
            if (n < left.Count)
            {
                leftOut = left[n].ToString("F2");
            }
            else
            {
                leftOut = "N/A";
            }
            if (n < right.Count)
            {
                rightOut = right[n].ToString("F2");
            }
            else
            {
                rightOut = "N/A";
            }
            debugText += leftOut + "\t" + rightOut + "\n";
        }
        return debugText;
    }

    private String getDebugOutput(List<Vector2> left)
    {
        String debugText = "";
        int rows = left.Count;

        for (int n = 0; n < rows; n++)
        {
            String leftOut = "";
            if (n < left.Count)
            {
                leftOut = left[n].ToString("F2");
            }
            else
            {
                leftOut = "N/A";
            }
            debugText += leftOut + "\n";
        }
        return debugText;
    }

    private void ShowDebugText(string text)
    {
        if (debugTextDisplay != null)
        {

            debugTextDisplay.UpdateDebugText(text);
        }
        if (debugText != null)
        {
            debugText.text = text;
        }
    }

    public void GenerateAndSaveImage(List<Vector2> points, int width, int height, string filePath)
    {
        Texture2D texture = VectorToImage.Vector2ListToTexture(points, width, height, Color.red, Color.black);
        VectorToImage.SaveTextureToFile(texture, filePath);
    }

    void TestImageStart()
    {
        List<Vector2> testPoints = new List<Vector2>
        {
            new Vector2(0.1f, 0.2f),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.9f, 0.8f)
        };

        GenerateAndSaveImage(testPoints, 256, 256, Application.dataPath + "/testImage.png");
    }
}
