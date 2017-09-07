using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Microsoft.Kinect;
using Coding4Fun.Kinect.WinForm;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections;
using Accord.Math;
using Accord.Statistics.Kernels;
using System.Windows.Controls;

namespace WindowsFormsApplication1
{
    public partial class Temp : Form
    {
        static private NotificationSender notifier = new NotificationSender();
        static private MessageSender messageSender = new MessageSender();

        private String username = String.Empty;
        private KinectSensor kinect;
        private const float MaxDepthDistance = 4000;
        private const float MinDepthDistance = 850;
        private const float MaxDepthDistanceOffset = MaxDepthDistance - MinDepthDistance;

        //SVM Model Object
        public SVMTest svm;
        FrameObject prevFrameObject;

        static Queue fallMessages = Queue.Synchronized(new Queue());

        private int frameCounter = 0;
        private int nullFrameCounter = 0;

        private List<double> fiveSecondData = new List<double>();
        public Bitmap scene;

        public static Temp _MainFrm;
        private Graphics g = null;
        private Skeleton body;

        public Temp(String user)
        {
            username = user;
            InitializeComponent();
            System.Windows.Forms.Application.DoEvents();
            _MainFrm = this;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // test method
            tbOutput.AppendText(notifier.SendNotification("Send to targeted users"));

        }

        /// <summary>
        ///  Get user data from the firebase database
        /// </summary>
        private void GetDataFromFirebase()

        {
            string uri = "https://myfirstapplication-5ad99.firebaseio.com/users/6b4rvGyIgMgQMo1XQBVKglI5k7l1.json/?auth=rngcgjOb25J68o1JW5XUEFigUbO86kNQmKxN4IB5";
            FirebaseRequest fq = new FirebaseRequest(uri, httpMethod.GET);
            fq.makeRequest();
            String res = fq.executeGetRequest();
            tbOutput.AppendText(res);
        }



        private void Temp_Load(object sender, EventArgs e)
        {
            if (KinectSensor.KinectSensors.Count <= 0)
            {
                MessageBox.Show("No kinect device found! Please connect kinect!");
            }
            else
            {
                kinect = KinectSensor.KinectSensors[0];
                lblConnectionID.Text = kinect.DeviceConnectionId;
                tbOutput.AppendText("Found kinectID: " + kinect.DeviceConnectionId + "\n");
            }
        }

        // Get the skeletal coordinates
        private void Kinect_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame f = e.OpenSkeletonFrame())
            {

                if (f != null)
                {
                    CreateSkeletonTrackingMap(f);
                    //var skeletons = new Skeleton[f.SkeletonArrayLength];
                    //f.CopySkeletonDataTo(skeletons);
                    //// Find the first person to track
                    //var trackedPerson = skeletons.FirstOrDefault(s => s.TrackingState == SkeletonTrackingState.Tracked);

                    //if (trackedPerson != null)
                    //{
                    //    List<double> data = new List<double>();

                    //    float maxY = -1000;
                    //    float minY = -1000;
                    //    float maxX = -1000;
                    //    float minX = -1000;
                    //    float maxZ = -1000;
                    //    float minZ = -1000;

                    //    float boxW = 0;
                    //    float boxH = 0;
                    //    float boxD = 0;

                    //    double headX = trackedPerson.Joints[JointType.Head].Position.X;
                    //    double headY = trackedPerson.Joints[JointType.Head].Position.Y;
                    //    double headZ = trackedPerson.Joints[JointType.Head].Position.Z;

                    //    float shoulderCenterX = trackedPerson.Joints[JointType.ShoulderCenter].Position.X;
                    //    float shoulderCenterY = trackedPerson.Joints[JointType.ShoulderCenter].Position.Y;
                    //    float shoulderCenterZ = trackedPerson.Joints[JointType.ShoulderCenter].Position.Z;

                    //    Vector3 shoulderCenter = new Vector3(shoulderCenterX, shoulderCenterY, shoulderCenterZ);

                    //    float hipCenterX = trackedPerson.Joints[JointType.HipCenter].Position.X;
                    //    float hipCenterY = trackedPerson.Joints[JointType.HipCenter].Position.Y;
                    //    float hipCenterZ = trackedPerson.Joints[JointType.HipCenter].Position.Z;

                    //    Vector3 hipCenter = new Vector3(hipCenterX, hipCenterY, hipCenterZ);

                    //    Vector3 spine = shoulderCenter - hipCenter;
                    //    spine.Normalize();

                    //    foreach (Joint joint in trackedPerson.Joints)
                    //    {
                    //        //Console.WriteLine(joint.JointType);
                    //        // Find the max and min coordiates in the X axis
                    //        if (maxY == -1000)
                    //        {
                    //            maxY = joint.Position.Y;
                    //            minY = maxY;
                    //            maxX = joint.Position.X;
                    //            minX = maxX;
                    //            maxZ = joint.Position.Z;
                    //            minZ = maxZ;
                    //        }
                    //        else
                    //        {
                    //            if (maxX < joint.Position.X)
                    //            {
                    //                maxX = joint.Position.X;
                    //            }
                    //            if (minX > joint.Position.X)
                    //            {
                    //                minX = joint.Position.X;
                    //            }
                    //            // Find the max and min cooridates in the Y axis
                    //            if (maxY < joint.Position.Y)
                    //            {
                    //                maxY = joint.Position.Y;
                    //            }
                    //            if (minY > joint.Position.Y)
                    //            {
                    //                minY = joint.Position.Y;
                    //            }
                    //            // Find the max and min cooridates in the Z axis
                    //            if (maxZ < joint.Position.Z)
                    //            {
                    //                maxZ = joint.Position.Z;
                    //            }
                    //            if (minZ > joint.Position.Z)
                    //            {
                    //                minZ = joint.Position.Z;
                    //            }
                    //        }
                    //    }

                    //    boxW = Math.Abs(maxX - minX); // The width of the bounding box 
                    //    boxH = Math.Abs(maxY - minY); // The height of the bounding box
                    //    boxD = Math.Abs(maxZ - minZ); // The depth of the bounding box

                    //    if (prevFrameObject == null || (f.Timestamp - prevFrameObject.Timestamp) > 1000)
                    //    {
                    //        prevFrameObject = new FrameObject(f.Timestamp, headX, headY, headZ, boxW, boxH, boxD, spine.X, spine.Y, spine.Z, hipCenterX, hipCenterY, hipCenterZ);
                    //        Console.WriteLine((f.Timestamp));
                    //        Console.WriteLine((prevFrameObject.Timestamp));
                    //    }
                    //    else
                    //    {
                    //        FrameObject newFrame = new FrameObject(f.Timestamp, headX, headY, headZ, boxW, boxH, boxD, spine.X, spine.Y, spine.Z, hipCenterX, hipCenterY, hipCenterZ);
                    //        // if classify create object, then change the prevFrame
                    //        if (svm == null)
                    //        {
                    //            // Change to training data location
                    //            svm = new SVMTest();
                    //            svm.buildModel();
                    //        }
                    //        //run algorithm 

                    //        SVMTest threadModel = new SVMTest((Accord.MachineLearning.VectorMachines.SupportVectorMachine<Gaussian>)svm.svmModel.Clone(),
                    //                                            prevFrameObject,
                    //                                            newFrame,
                    //                                            _MainFrm);

                    //        // start thread
                    //        BackgroundWorker bg = new BackgroundWorker();
                    //        bg.DoWork += new DoWorkEventHandler(bg_DoWork);
                    //        bg.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bg_RunWorkerCompleted);
                    //        bg.RunWorkerAsync(threadModel);

                    //        //Thread threadClassify = new Thread(new ThreadStart(threadModel.classify));
                    //        //threadClassify.Start();

                    //        prevFrameObject = newFrame;
                    //    }
                    //}
                }
            }
        }


        private void Kinect_DepthFrameReady(object sender, DepthImageFrameReadyEventArgs e)
        {
            using (DepthImageFrame f = e.OpenDepthImageFrame())
            {
                if (f != null)
                {
                    depthImage.Image = f.CreateDepthBitMap();
                }
            }
        }


        private void Kinect_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (ColorImageFrame f = e.OpenColorImageFrame())
            {
                if (f != null)
                {
                    Bitmap map = f.CreateBitMapFromSensor();
                    scene = map;
                    videoBox.Image = map;
                }
            }
        }

        private void CreateSkeletonTrackingMap(SkeletonFrame f)
        {
            //list of skeletons
            var skeletons = new Skeleton[f.SkeletonArrayLength];
            f.CopySkeletonDataTo(skeletons);

            //Find the first person to track
            Skeleton trackedPerson = skeletons.FirstOrDefault(s => s.TrackingState == SkeletonTrackingState.Tracked);

            if (trackedPerson != null)
            {
                if (trackedPerson.TrackingState == SkeletonTrackingState.Tracked)
                {
                    this.body = trackedPerson;
                    skeletonImage.Refresh();
                }
            }

        }

        private void KinectSensors_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            this.lblStatus.Text = kinect.Status.ToString();
        }

        public void AppendToBox(String text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendToBox), new object[] { text });
                return;
            }
            this.tbOutput.AppendText(text);
        }

        private void colorBtn_Click(object sender, EventArgs e)
        {
            if (colorBtn.Text.Equals("Color ON"))
            {
                if (!kinect.IsRunning)
                {
                    kinect.Start();

                    tbOutput.AppendText("Started Kinect!\n");
                }

                kinect.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                kinect.ColorFrameReady += Kinect_ColorFrameReady;
                // Set btnColor to Off
                colorBtn.Text = "Color OFF";
                this.Update();
            }
            else
            {
                kinect.ColorFrameReady -= Kinect_ColorFrameReady;
                kinect.ColorStream.Disable();
                videoBox.Image = null;
                colorBtn.Text = "Color ON";
            }
        }

        private void depthBtn_Click(object sender, EventArgs e)
        {
            if (depthBtn.Text.Equals("Depth ON"))
            {

                if (!kinect.IsRunning)
                {
                    kinect.Start();
                    tbOutput.AppendText("Started Kinect!\n");
                }
                kinect.DepthStream.Enable();
                kinect.DepthStream.Range = DepthRange.Default;
                kinect.DepthFrameReady += Kinect_DepthFrameReady;
                depthBtn.Text = "Depth OFF";
            }
            else
            {
                kinect.DepthFrameReady -= Kinect_DepthFrameReady;
                kinect.DepthStream.Disable();
                depthImage.Image = null;
                depthBtn.Text = "Depth ON";
                this.Update();
            }
        }

        private void skeletonBtn_Click(object sender, EventArgs e)
        {
            if (skeletonBtn.Text.Equals("Skeleton ON"))
            {
                if (!kinect.IsRunning)
                {
                    kinect.Start();
                    tbOutput.AppendText("Started Kinect!\n");
                }
                    kinect.SkeletonStream.Enable();
                    //kinect.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
                    kinect.SkeletonFrameReady += Kinect_SkeletonFrameReady;
                    skeletonBtn.Text = "Skeleton OFF";
                    skeletonImage.BackColor = System.Drawing.Color.Black;
                    this.Update();
            }
            else
            {
                kinect.SkeletonStream.Disable();
                //kinect.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
                kinect.SkeletonFrameReady -= Kinect_SkeletonFrameReady;
                body = null;
                skeletonBtn.Text = "Skeleton ON";
                skeletonImage.BackColor = System.Drawing.Color.White;
                this.Update();
            }
        }

        private void graphBtn_Click(object sender, EventArgs e)
        {

        }

        private void skeletonImage_Paint(object sender, PaintEventArgs e)
        {
            g = skeletonImage.CreateGraphics();
            if (body != null)
            {
                skeletonImage.DrawSkeleton(body, g);
            }
        }


        private void sendMessageButton_Click(object sender, EventArgs e)
        {
            String message = messageTb.Text;
            Bitmap copy = new Bitmap(scene);
            messageSender.sendMessageToAllContact(message, scene);
        }

        #region classify worker
        static void bg_DoWork(object sender, DoWorkEventArgs e)
        {
            SVMTest threadObj = e.Argument as SVMTest;
            bool result = threadObj.classify();
            fallMessages.Enqueue(result);
        }

        static void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int windowSize = 5;
            int count = 0;

            lock (fallMessages)
            {
                if (fallMessages.Count >= windowSize)
                {
                    for (int i = 0; i < windowSize; i++)
                    {
                        if ((bool)fallMessages.Dequeue())
                        {
                            count++;
                        }
                    }

                    if (count == windowSize)
                    {
                        _MainFrm.AppendToBox("Fall Detected!");

                        // Add code here to send notification and messages
                        _MainFrm.AppendToBox(notifier.SendNotification("Send to targeted users"));

                        String message = "Fall Detected! Please check messages for more details.";
                        messageSender.sendMessageToAllContact(message, _MainFrm.scene);
                    }
                }
            }
        }

        #endregion

        #region update ui worker
        private void updateUIWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            
        }
        #endregion
    }
}

