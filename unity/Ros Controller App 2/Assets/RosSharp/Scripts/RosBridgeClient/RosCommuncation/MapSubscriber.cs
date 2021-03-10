/*
© Siemens AG, 2018
Author: Berkay Alp Cakal (berkay_alp.cakal.ct@siemens.com)

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
using System.Threading;
using System.Threading.Tasks;
using PGM;
using RosSharp.RosBridgeClient.MessageTypes.Nav;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class MapSubscriber : UnitySubscriber<MessageTypes.Nav.OccupancyGrid>
    {
        public MapReader mapReader;

        protected override void Start()
        {
            base.Start();
        }

        protected override void ReceiveMessage(OccupancyGrid message)
        {
            if (mapReader.visualizeMap)
            {
                Task.Run(() => mapReader.VisualizeMap(message));
            }
        }
    }
}