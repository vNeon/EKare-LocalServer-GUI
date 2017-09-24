# EKare Local Server with GUI

This is the local server with a graphical user interface. The GUI allowed for easier debugging and developement, and then later ported to a windows service. 

It is responsible for:
* Collecting Skeletal tracking frames from the Microsoft Kinect.
* Fall Detection, by running a machine learning algorithm.
* Notifying a cloud serivce that a fall has occured.

This is implementated as a windowsform application.

## Authors: 

### Johnny Pham 
* UPI: dpha010
* GithubID: JohPa8696
### Vincent Nio
* UPI: vnio666
* GithubID: vNeon

## Prerequisites
* .NET
* Kinect for [Windows SDK v1.8](https://www.microsoft.com/en-nz/download/details.aspx?id=40278).

## Installation - Run
1. Clone/Download the project from github using the command:

    * git clone https://github.com/vNeon/EKare-LocalServer-GUI.git
  
    * Or download the zip file from https://github.com/vNeon/EKare-LocalServer-GUI.git
  
2. Open the project using Visual studio
    * Navigate to the GitHub project and double click on the file "WindowsFormsApplication1.sln".
The file "WindowsFormsApplication1.sln" should be in the same directory as this README.md file.

3. Plugin your Kinect device.

4. Run the application by clicking "start".

## GUI Buttons

1. Color ON/OFF 
    * toggles the RGB colour sensor of the Kinect. Shows color image.
    
2. Depth ON/OFF 
    * toggles the Depth sensor of the Kinect. Shows depth image.
    
3. Skeleton ON/OFF
    * toggles the Fall detection algorithm. Shows the skeletal tracking.
    
4. Send Notifiaction 
    * sends Push notification to android users.
    
5. Send Message 
    * sends message to android users.
