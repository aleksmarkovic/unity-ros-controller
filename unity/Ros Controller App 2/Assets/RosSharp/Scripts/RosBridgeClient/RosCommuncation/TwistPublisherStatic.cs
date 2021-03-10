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

using UnityEngine;
using UnityEngine.UI;

namespace RosSharp.RosBridgeClient
{
    public class TwistPublisherStatic : UnityPublisher<MessageTypes.Geometry.Twist>
    {
        [SerializeField] private Button buttonUp, buttonDown, buttonRight, buttonLeft;
        
        private MessageTypes.Geometry.Twist message;
        private double linearStep = 0.05;
        private double angularStep = 0.02;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }


        private void InitializeMessage()
        {
            message = new MessageTypes.Geometry.Twist
            {
                linear = new MessageTypes.Geometry.Vector3(), angular = new MessageTypes.Geometry.Vector3()
            };
        }
        
        public void LinearClickUp()
        {
            message.linear.x += linearStep;

            if (message.linear.x < 0)
            {
                message.linear.x = 0;
                message.angular.z = 0;
            }

            Publish(message);
        }
        
        public void LinearClickDown()
        {
            message.linear.x -= linearStep;

            if (message.linear.x > 0)
            {
                message.linear.x = 0;
                message.angular.z = 0;
            }

            Publish(message);
        }
        
        public void AngularClickRight()
        {
            message.angular.z -= angularStep;
            Publish(message);
        }
        
        public void AngularClickLeft()
        {
            message.angular.z += angularStep;
            Publish(message);
        }
    }
}
