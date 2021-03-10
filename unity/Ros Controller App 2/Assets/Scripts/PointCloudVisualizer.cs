using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class PointCloudVisualizer : MonoBehaviour
{
    public Material material;
    public bool newMessage;
    public bool  createMap;
    public RosSharp.RosBridgeClient.MessageTypes.Custom.CustomPointCloudMsg customPointCloudMsg;
    public GameObject rosConnector;

    private GameObject octoSpheres;
    private bool mapping, processRunning;
    private int lastPointCount, spawnsPerFrame;
    private List<string>  octoDataPoints;
    private List<RosSharp.RosBridgeClient.MessageTypes.Custom.CustomPointCloud> pendingOctoDataPoints;

    private void Start()
    {
        mapping = false;
        processRunning = false;
        octoSpheres = new GameObject("octoSpheres");
        octoDataPoints = new List<string>();
        pendingOctoDataPoints = new List<RosSharp.RosBridgeClient.MessageTypes.Custom.CustomPointCloud>();
    }

    private void Update()
    {
        if (newMessage && mapping)
        {
            newMessage = false;

            if (customPointCloudMsg.dataPoints.Length > lastPointCount && !processRunning)
            {
                MapPrep();
            }
        }
    }        

    private async void MapPrep()
    {
        processRunning = true;

        await Task.Run(() => CheckExistingPoints());

        Create3DMap();

        processRunning = false;
    }
    
    private void Create3DMap()
    {
        foreach (var pendingData in pendingOctoDataPoints)
        {
            var newName = "" + -pendingData.y + pendingData.z + pendingData.x;

            var newPrimitive = GameObject.CreatePrimitive(PrimitiveType.Cube);
           
            newPrimitive.transform.parent = octoSpheres.transform;
            var objRendererMaterial = newPrimitive.GetComponent<Renderer>().material;
            objRendererMaterial = material;
            objRendererMaterial.SetColor("_Color", Color.cyan);

            newPrimitive.transform.localScale = 0.05f * Vector3.one;
            newPrimitive.transform.localPosition = new Vector3(-pendingData.y, pendingData.z, pendingData.x);

            newPrimitive.name = newName;

            octoDataPoints.Add(newName);
        }

        lastPointCount = customPointCloudMsg.dataPoints.Length;
        pendingOctoDataPoints.Clear();
    }

    private void CheckExistingPoints()
    {
        for (int i = 0; i < customPointCloudMsg.dataPoints.Length; i++)
        {
            if (customPointCloudMsg.dataPoints[i].z < 0.10f) continue;

            var newName = "" + -customPointCloudMsg.dataPoints[i].y +
                customPointCloudMsg.dataPoints[i].z + customPointCloudMsg.dataPoints[i].x; ;

            if (octoDataPoints.FirstOrDefault(o => o.Equals(newName)) != null) continue;

            pendingOctoDataPoints.Add(customPointCloudMsg.dataPoints[i]);
        }
    }

    public void TurnOnOff3DMapping()
    {
        mapping = !mapping;

        if (!mapping)
            Destroy(octoSpheres);
        else
            octoSpheres = new GameObject("octoSpheres");
    }
}


//private void Create3DMap()
//{
//    for (int i = 0; i < customPointCloudMsg.dataPoints.Length; i++)
//    {
//        if (customPointCloudMsg.dataPoints[i].z < 0.10f) continue;

//        var newName = "" + -customPointCloudMsg.dataPoints[i].y +
//            customPointCloudMsg.dataPoints[i].z + customPointCloudMsg.dataPoints[i].x; ;

//        if (tmpList.FirstOrDefault(s => s.name.Equals(newName)) != null) continue;

//        var newPrimitive = GameObject.CreatePrimitive(PrimitiveType.Cube);

//        newPrimitive.transform.parent = octoSpheres.transform;
//        var objRendererMaterial = newPrimitive.GetComponent<Renderer>().material;
//        objRendererMaterial = material;
//        objRendererMaterial.SetColor("_Color", Color.cyan);

//        newPrimitive.transform.localScale = 0.05f * Vector3.one;
//        newPrimitive.transform.localPosition = new Vector3(-customPointCloudMsg.dataPoints[i].y,
//            customPointCloudMsg.dataPoints[i].z, customPointCloudMsg.dataPoints[i].x);

//        newPrimitive.name = newName;

//        octoDataPoints.Add(newPrimitive);
//    }

//    lastPointCount = customPointCloudMsg.dataPoints.Length;
//    pendingOctoDataPoints.Clear();
//}