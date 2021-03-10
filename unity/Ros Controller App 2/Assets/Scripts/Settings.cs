using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiRobot
{
    public string RobotsPrefix { get; set; }
    public int NumberOfRobots { get; set; }
}
public class Settings : MonoBehaviour
{

    public static Settings SettingsInstance;

    public string ipConfig;
    public MultiRobot MultiRobot;
    
    [SerializeField] private InputField ipInputField;
    [SerializeField] private InputField prefixInputField;
    [SerializeField] private Button cameraButton;
    [SerializeField] private Button mappingButton;

    private void Awake()
    {
        if (SettingsInstance != null)
        {
            Destroy(SettingsInstance);
        }
        else
        {
            SettingsInstance = this;
            DontDestroyOnLoad(this);

            cameraButton.onClick.AddListener(LoadControl);
            mappingButton.onClick.AddListener(Load3DMapping);

            ipConfig = PlayerPrefs.GetString("ipConfig", "ws://192.168.8.101:9090");
        }
    }

    private void Start()
    {
        // Make the game run as fast as possible
        Application.targetFrameRate = 300;
    }

    private void LoadControl()
    {
        SceneManager.LoadScene("Control");
    }

    private void Load3DMapping()
    {
        SceneManager.LoadScene("3d Mapping");
    }

    public void SaveHost()
    {
        var inputText = ipInputField.text;
        ipConfig = "ws://" + inputText + ":9090";
        PlayerPrefs.SetString("ipConfig", ipConfig);
        
        if (prefixInputField.text.Length > 0)
            SaveMultiRobot();
    }

    private void SaveMultiRobot()
    {
        MultiRobot = new MultiRobot
        {
            RobotsPrefix = prefixInputField.text, NumberOfRobots = 2
        };
    }
}