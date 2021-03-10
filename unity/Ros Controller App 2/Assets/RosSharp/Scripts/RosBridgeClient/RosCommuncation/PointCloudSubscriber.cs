/*
© Siemens AG, 2017-2018
Author: Dr. Martin Bischoff (martin.bischoff@siemens.com)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
<http://www.apache.org/licenses/LICENSE-2.0>.
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class PointCloudSubscriber : UnitySubscriber<MessageTypes.Custom.CustomPointCloudMsg>
    {
        //  public Transform PublishedTransform;
        public PointCloudVisualizer pointCloudVisualizer;

        private Vector3 position;
        private Quaternion rotation;
        private bool isMessageReceived, getMessage;
        

        protected override void Start()
        {
            base.Start();                       
        }

        protected override void ReceiveMessage(MessageTypes.Custom.CustomPointCloudMsg message)
        {
            //if (pointCloudVisualizer.createMap)
            //{ 
                pointCloudVisualizer.customPointCloudMsg = message;
                pointCloudVisualizer.newMessage = true;
            //}
        }

        private void ProcessMessage()
        {
           // PublishedTransform.position = position;

            //   PublishedTransform.position = new Vector3(position.x, position.z, -position.y);
            //   PublishedTransform.rotation = new Quaternion(rotation.y, -rotation.z, -rotation.x, rotation.w);
           // PublishedTransform.rotation = rotation;
            //  mapRobotPositioner.SetPosition(position);

            //             return new Quaternion(quaternion.y, -quaternion.z, -quaternion.x, quaternion.w);

        }


        //private Vector3 GetPosition(MessageTypes.Nav.Odometry message)
        //{
        //    return new Vector3(
        //        (float)message.pose.pose.position.x,
        //        (float)message.pose.pose.position.y,
        //        (float)message.pose.pose.position.z);
        //}

        //private Quaternion GetRotation(MessageTypes.Nav.Odometry message)
        //{
        //    return new Quaternion(
        //        (float)message.pose.pose.orientation.x,
        //        (float)message.pose.pose.orientation.y,
        //        (float)message.pose.pose.orientation.z,
        //        (float)message.pose.pose.orientation.w);
        //}
    }
}