using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBrightness : MonoBehaviour
{
    // Start is called before the first frame update
    public CameraBrightness soCamera;
    public Canvas canvas;
    [Tooltip("This is based on the near clip plane of the camera")]
    public float offSetPlaneDistance = 0.2f;
    void Start()

    {
       // canvas.worldCamera = soCamera.gameObject.GetComponent<Camera>();
       // canvas.planeDistance = soCamera.gameObject.GetComponent<Camera>().nearClipPlane + offSetPlaneDistance;
    }

    
}
