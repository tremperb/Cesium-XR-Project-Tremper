# Cesium XR Interview Project

This project is created using Unity3D for the Oculus Quest(1st Gen. Tested). (Version: 2019.4.3f1 LTS)

The source code can be found in the 'Cesium-XR-Terrain-Unity-Project' folder. You can simply view the files directly from the asset folder here or download the full Unity project and view on your machine using the Unity version listed above.

This application can be run by downloading the included APK file and sideloading it to your quest device where you can find it in your unknown files folder. Or alternatively if you downloaded the entire Unity project you can run the project in the Unity play mode by connecting an Oculus Link cable to your device and enabling oculus desktop(Here you can test in play mode or simply build the project to your device, granted all the needed setting carried over to github). 

A video demoing this project can be found here: https://youtu.be/eP_qCzppsg4 

# Description
The main goal that I implemented was to create a way to view the terrain at any angle up close via the use of an interactable map. This places the terrain on a block where you can adjust the position(x & z), scale, and rotation. Due to the time constraint this is controlled through the use of sliders with more time this could be implemented by simply grabbing the terrain and moving it however you like. The main goal to overcome with this is restricting the terrain to be rendered when leaving the bounds of the block for the main. This was implemented by creating a custom vertex shader which simply discards the color black. Thus when a vertex of the terrain leaves the bounds of the map that vertex is then colored black where the shader will then discard it. 

As a side feature to this, a character controller was created allowing the user to walk around both the map to see it at any angle if desired and also walk around on  terrain model allowing them to actually explore this. 


# Requirements Overview

For this project, we would like you to develop an XR application for viewing terrain data. You
may use any framework or library you already have experience in. The final deliverables will be:

	1. The source code (can be a link to a repository)
	2. Instructions on how we can run the app
	3. A video capture showing your application running on an XR device

You should deploy your application to an XR device of your choice, whether it’s a VR headset or
a smartphone with AR capabilities. Alternatively, submit a project that emulates the interface of
an XR device.


# Data

Use the included terrain data to create your app.
	Mesh: XR 3D Software Developer - Terrain.glb
	Albedo: XR 3D Software Developer - Albedo.png


# Goal

The objective of this project is to make the terrain easy to explore in XR. For example, a user
may wish to see a particular section of the terrain close up, or they may want to look at the
entire landscape as a whole. They may even want to get all the way down to ground level and
feel as though they are walking on the terrain. Your goal is to achieve this with an intuitive
control scheme that takes advantage of the capabilities of XR.

You are free to implement additional features if you like, but this is not required. Some examples
of other user stories you could work on are:

	1. The user can see information about the terrain, e.g., slope, height.
	2. The user can put elements on the terrain, e.g., markers, vehicles.
	3. The user can see a cross-section of the terrain, e.g., clipping plane.
	4. The user can get a guided tour of the terrain, e.g., flythrough, points of interest.



