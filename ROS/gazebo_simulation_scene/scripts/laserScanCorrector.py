#!/usr/bin/env python

# Siemens AG, 2018
# Author: Berkay Alp Cakal (berkay_alp.cakal.ct@siemens.com)
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
# <http://www.apache.org/licenses/LICENSE-2.0>.
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.

import rospy
import numpy
from sensor_msgs.msg import LaserScan

def laserScanCorrector():
	# initialize node
	rospy.init_node('laserScanCorrector', anonymous=True)

	# setup /scan topic subscription
	scan_subscriber = rospy.Subscriber("/scan", LaserScan, handleScanMsg, queue_size=10)

	# spin() simply keeps python from exiting until this node is stopped
	rospy.spin()

def handleScanMsg(data):
	rospy.loginfo("\n/scan is now being handled...")
	#### Setup Corrected-LaserScan Publisher 
	correctedScan_publisher = rospy.Publisher("/scanCorrected", LaserScan, queue_size=10)
	correctedScan_msg = LaserScan()
	
	#### Start copying the variables
	correctedScan_msg.angle_min = data.angle_min
	correctedScan_msg.angle_max = data.angle_max
	correctedScan_msg.angle_increment = data.angle_increment
	correctedScan_msg.time_increment = data.time_increment
	correctedScan_msg.scan_time = data.scan_time
	correctedScan_msg.range_min = data.range_min
	correctedScan_msg.range_max = data.range_max
	correctedScan_msg.intensities = data.intensities

	#### Start copying 'ranges' using the corrected values if necessary
		# if (+inf) -> change it to (-1)

	correctedScan_msg.ranges = [0] * len(data.ranges)

	for i in range(len(data.ranges)):
		if numpy.isinf(data.ranges[i]):
			correctedScan_msg.ranges[i] = -100
		else:
			correctedScan_msg.ranges[i] = data.ranges[i]		

	#### Publish msg
	rate = rospy.Rate(1800) # 1800hz
	correctedScan_publisher.publish(correctedScan_msg)
	rospy.loginfo("\n/scanCorrected is now being published...")
	rate.sleep()

if __name__ == '__main__':
	laserScanCorrector()