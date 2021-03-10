using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera firstPersonCamera;
    public Camera overheadCamera;
    public GameObject cameraImage;

    public void ChangeCamera()
    {
      overheadCamera.enabled = !overheadCamera.enabled;
      firstPersonCamera.enabled = !firstPersonCamera.enabled;
      cameraImage.SetActive(!cameraImage.activeSelf);
    }
}
