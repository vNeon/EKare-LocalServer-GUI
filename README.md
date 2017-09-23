# WFA-SendMessageToApp

This GitHub project contains a GUI version of the windows service.

It is responsible for:
1. Collecting Skeletal tracking frames from the Microsoft Kinect.
2. Fall Detection, by running a machine learning algorithm.
3. Notifying a cloud serivce that a fall has occured.

This is implementated as a windowsform application.


## How to run

1. Download the GitHub project into a folder.
2. Download [visual studio's 2015](https://www.visualstudio.com/) or higher.
3. Download Kinect for [Windows SDK v1.8](https://www.microsoft.com/en-nz/download/details.aspx?id=40278).
4. Open visual studio's
5. Click "File" -> "Open" -> "Project/Solution..." or shortcut "Ctrl + Shift + O"
6. Navigate to the GitHub project and double click on the file "WindowsFormsApplication1.sln".
The file "WindowsFormsApplication1.sln" should be in the same directory as this README.md file.
7. Plugin your Kinect device.
8. Click the Start button to run the application.
9. It will ask for a email and password. It should already be entered so just click the Login Button. 
If this does not work you will need to create an account using the Android application.

### GUI Buttons

* Color ON/OFF - toggles the RGB colour sensor of the Kinect.Shows color image.
* Depth ON/OFF - toggles the Depth sensor of the Kinect. Shows depth image.
* Skeleton ON/.OFF- toggles the Fall detection algorithm. Shows the skeletal tracking.
