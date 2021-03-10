using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    [SerializeField]
    private GameObject visualizationObject;
    private Button button;

    private void Awake()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(ToggleObject);    
    }

    private void ToggleObject()
    {
        visualizationObject.SetActive(!visualizationObject.activeSelf);
    }    
}
