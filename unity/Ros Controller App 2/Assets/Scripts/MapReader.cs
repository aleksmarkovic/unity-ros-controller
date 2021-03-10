using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RosSharp.RosBridgeClient.MessageTypes.Nav;
using UnityEngine;
using UnityEngine.UI;

namespace PGM
{
    public class MapReader : MonoBehaviour
    {
        public Image imageToUpdate;
        public bool visualizeMap;
        
        private Texture2D texture;
        private Color[] mapColorArray;
        private bool setMap;
        private float mapWidth, mapHeight;
        
        private void Awake()
        {
            setMap = false;
            texture = new Texture2D(0, 0);
        }

        private void Update()
        {
            if (setMap)
            {
                setMap = false;
                
                texture.Resize((int)mapWidth, (int)mapHeight);
                texture.SetPixels(mapColorArray);
                texture.Apply();
            
                GetComponent<Renderer>().material.mainTexture = texture;
            }
        }

        public void VisualizeMap(OccupancyGrid message)
        {
            try
          {
              var mapByteArray = message.data;

              mapWidth = message.info.width;
              mapHeight = message.info.height;
        
              mapColorArray = new Color[mapByteArray.Length];
        
              for (var i = 0; i < mapByteArray.Length; i++)
              {
                  Color color;
                  
                  switch (mapByteArray[i])
                  {    
                      default:
                      case -1:
                          continue;
                      case 0:
                          color = new Color((float)0.5,(float)0.5, (float)0.5);
                          break;
                      case 100: 
                          color = new Color(0, 0, 1);
                          break;
                  }
                  mapColorArray[i] = color;
              }
        
              setMap = true;
          }
          catch (Exception e)
          {
              Debug.Log(e.Message);
          }
        }

        public void VisualizeMapOnOff()
        {
            visualizeMap = !visualizeMap;

            if (!visualizeMap)
            {
                texture.Resize(0, 0);
                texture.Apply();

                GetComponent<Renderer>().material.mainTexture = texture;
            }
        }
    }
}