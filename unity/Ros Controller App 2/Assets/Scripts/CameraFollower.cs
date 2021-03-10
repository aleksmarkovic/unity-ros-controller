using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private Vector3 robotPosition = new Vector3(0,0,0);
    public Transform robotObject;
    
    public float smooth= 5.0f;
    void  Update ()
    {
        var newPosition = new Vector3(robotObject.position.x, transform.position.y, robotObject.position.z);
        transform.position = Vector3.Lerp (
            transform.position, newPosition,
            Time.deltaTime * smooth);
    } 
}
