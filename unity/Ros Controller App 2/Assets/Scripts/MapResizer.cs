using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapResizer : MonoBehaviour
{
    [SerializeField] private GameObject map;

    private RectTransform mapRectTransform;
    private Button sizeUp, sizeDown;
    
    const float RESIZE_VALUE = (float)0.1;
    
    private void Awake()
    {
        sizeUp = transform.GetChild(0).GetComponent<Button>();
        sizeDown = transform.GetChild(1).GetComponent<Button>();
        
        sizeUp.onClick.AddListener(SizeUpMap);
        sizeDown.onClick.AddListener(SizeDownMap);

        mapRectTransform = map.GetComponent<RectTransform>();
    }

    private void SizeUpMap()
    {
       mapRectTransform.transform.localScale += new Vector3(RESIZE_VALUE,  RESIZE_VALUE, 1);
       // mapRectTransform.transform.localPosition = new Vector3(0,0,0);
    }
    
    private void SizeDownMap()
    {
       mapRectTransform.transform.localScale -= new Vector3(RESIZE_VALUE, RESIZE_VALUE, 1);
       // mapRectTransform.transform.localPosition = new Vector3(0,0,0);
    }
}
