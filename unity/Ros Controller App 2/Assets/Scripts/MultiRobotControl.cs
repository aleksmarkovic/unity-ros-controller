using System;
using System.Collections;
using System.Collections.Generic;
using RosSharp.RosBridgeClient;
using UnityEngine;
using UnityEngine.UI;

public class MultiRobotControl : MonoBehaviour
{
    [SerializeField] private GameObject[] rosConnector;
    [SerializeField] private GameObject[] robotModel;
    [SerializeField] private Button multiRobotButton;
    
    private ImageSubscriber activeRobotCamera;
    private ImageSubscriber inactiveRobotCamera;

    private void Awake()
    {
        activeRobotCamera = rosConnector[0].GetComponent<ImageSubscriber>();
        if (Settings.SettingsInstance.MultiRobot != null)
        {
             robotModel[1].SetActive(true);
             rosConnector[1].SetActive(true);
             
             inactiveRobotCamera = rosConnector[1].GetComponent<ImageSubscriber>();
             inactiveRobotCamera.enabled = false;
        }
        else
        {
            multiRobotButton.interactable = false;
        }
    }

    public void SwitchRobotCamera()
    {
        activeRobotCamera.enabled = false;
        inactiveRobotCamera.enabled = true;

        var tmp = activeRobotCamera;
        activeRobotCamera = inactiveRobotCamera;
        inactiveRobotCamera = tmp;
    }
}
