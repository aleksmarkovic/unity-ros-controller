using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsCounter : MonoBehaviour
{
    private Text text;

    public static int Fps;

    private void Awake()
    {
        text = gameObject.GetComponent<Text>();
    }

    private void Update()
    {
        var current = 0;
        current = (int)(1f / Time.unscaledDeltaTime);
        Fps = (int)current;
        text.text = "FPS: " + Fps.ToString();
    }
}
