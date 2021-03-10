using System.Collections;
using System.Collections.Generic;
using RosSharp.RosBridgeClient;
using UnityEngine;
using UnityEngine.UI;

public class MovementButton : MonoBehaviour
{
    [SerializeField] private TwistPublisherStatic twistPublisherStatic;

    public void ButtonAction()
    {
        switch (gameObject.name)
        {
            case "Up":
                twistPublisherStatic.LinearClickUp();
                break;
            case "Down":
                twistPublisherStatic.LinearClickDown();
                break;
            case "Right":
                twistPublisherStatic.AngularClickRight();
                break;
            case "Left":
                twistPublisherStatic.AngularClickLeft();
                break;
        }
    }
}
