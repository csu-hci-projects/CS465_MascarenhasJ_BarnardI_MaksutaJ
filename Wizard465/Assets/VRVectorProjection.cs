using System.Collections.Generic;
using UnityEngine;

public class VRVectorProjection : MonoBehaviour
{
    public Camera mainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<Vector2> vector3PathToVector2ByPerspectiveDivide(List<Vector3> vector3Path)
    {
        List<Vector2> result = new List<Vector2>();

        foreach (Vector3 vector3 in vector3Path)
        {
            result.Add(new Vector2(vector3.x / vector3.z, vector3.y / vector3.z));
        }

        return result;
    }

    public List<Vector2> ProjectVector3PathToVector2(List<Vector3> vector3Path)
    {
        return ProjectVector3PathToVector2(vector3Path, mainCamera);
    }

    public List<Vector2> ProjectVector3PathToVector2(List<Vector3> vector3Path, Camera mainCamera)
    {
        List<Vector2> result = new List<Vector2>();
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera is not assigned!");
            return result;
        }

        foreach (Vector3 vector3 in vector3Path)
        {
            Vector3 screenPoint = mainCamera.WorldToScreenPoint(vector3);
            result.Add(new Vector2(screenPoint.x, screenPoint.y));
        }

        return result;
    }

    public List<Vector2> ProjectVector3PathToViewport(List<Vector3> vector3Path)
    {
        return ProjectVector3PathToViewport(vector3Path, mainCamera);
    }

    public List<Vector2> ProjectVector3PathToViewport(List<Vector3> vector3Path, Camera mainCamera)
    {
        List<Vector2> result = new List<Vector2>();
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera is null");
            return result;
        }

        foreach (Vector3 vector3 in vector3Path)
        {
            Vector3 viewportPoint = mainCamera.WorldToViewportPoint(vector3);
            //result.Add(new Vector2(viewportPoint.x / viewportPoint.z, viewportPoint.y / viewportPoint.z));
            result.Add(new Vector2(viewportPoint.x, viewportPoint.y));
        }

        return result;
    }
}
