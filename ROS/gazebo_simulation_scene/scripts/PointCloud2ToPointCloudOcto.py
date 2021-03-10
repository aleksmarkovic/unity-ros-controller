#!/usr/bin/env python

import rospy
import ros_numpy
import numpy as np
from sensor_msgs.msg import PointCloud2
from sensor_msgs.msg import Image
from custom_point_cloud_msg.msg import CustomPointCloudMsg
from custom_point_cloud_msg.msg import CustomPointCloud

import std_msgs.msg

def pointGetter():
	# initialize node
	rospy.init_node('pointGetter', anonymous=True)

	# setup /scan topic subscription
	scan_subscriber = rospy.Subscriber("/octomap_point_cloud_centers", PointCloud2, getPoints, queue_size=10)
#/
	# spin() simply keeps python from exiting until this node is stopped
	rospy.spin()

def getPoints(data):
	newPoints = rospy.Publisher("/octomap_point_cloud_centers_filtered", CustomPointCloudMsg, queue_size=10)

	newMsg = CustomPointCloudMsg()

	xyz_array = ros_numpy.point_cloud2.pointcloud2_to_array(data)
	rospy.loginfo("\n/Start Point")

	newMsg.header.stamp = rospy.Time.now()
	newMsg.header.frame_id = "octomap_point_cloud_centers_filtered"


	for element in xyz_array:
		newData = CustomPointCloud()

		newData.x = float(element[0])
		newData.y = float(element[1])
		newData.z = float(element[2])

		newMsg.dataPoints.append(newData)


	rate = rospy.Rate(1800) 
	newPoints.publish(newMsg)
	rate.sleep()


if __name__ == '__main__':
	pointGetter()