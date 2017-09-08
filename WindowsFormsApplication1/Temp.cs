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
        public SupportVectorMachine svm;
        FrameObject prevFrameObject;

        static Queue fallMessages = Queue.Synchronized(new Queue());

        private int frameCounter = 0;
        private int nullFrameCounter = 0;

        private List<double> fiveSecondData = new List<double>();
        public Bitmap scene;

        public static Temp _MainFrm;
        private Graphics g = null;
        private Skeleton body;
        private List<FrameObject> frameList = new List<FrameObject>();
        private static Bitmap colorImage;
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
            tbOutput.AppendText(notifier.SendNotification());

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
                    var skeletons = new Skeleton[f.SkeletonArrayLength];
                    f.CopySkeletonDataTo(skeletons);
                    // Find the first person to track
                    var trackedPerson = skeletons.FirstOrDefault(s => s.TrackingState == SkeletonTrackingState.Tracked);

                    if (trackedPerson != null)
                    {

                        if (trackedPerson.TrackingState == SkeletonTrackingState.Tracked)
                        {
                            this.body = trackedPerson;
                            skeletonImage.Refresh();
                        }

                        frameCounter++;
                        if (frameCounter == 1 || frameCounter % 15 == 0)
                        {
                            List<double> data = new List<double>();

                            double headX = trackedPerson.Joints[JointType.Head].Position.X;
                            double headY = trackedPerson.Joints[JointType.Head].Position.Y;
                            double headZ = trackedPerson.Joints[JointType.Head].Position.Z;

                            float hipCenterX = trackedPerson.Joints[JointType.HipCenter].Position.X;
                            float hipCenterY = trackedPerson.Joints[JointType.HipCenter].Position.Y;
                            float hipCenterZ = trackedPerson.Joints[JointType.HipCenter].Position.Z;

                            Vector3 hipCenter = new Vector3(hipCenterX, hipCenterY, hipCenterZ);

                            float spineX = trackedPerson.Joints[JointType.Spine].Position.X;
                            float spineY = trackedPerson.Joints[JointType.Spine].Position.Y;
                            float spineZ = trackedPerson.Joints[JointType.Spine].Position.Z;

                            Vector3 spine = new Vector3(hipCenterX, hipCenterY, hipCenterZ);

                            double floorA = f.FloorClipPlane.Item1;
                            double floorB = f.FloorClipPlane.Item2;
                            double floorC = f.FloorClipPlane.Item3;
                            double floorD = f.FloorClipPlane.Item4;

                            // Update previous frame after 0.5 second of not detecting anyone in the area
                            if (frameList.Count == 3 || (frameList.Count > 0 && (f.Timestamp - frameList.Last().Timestamp) > 1000))
                            {
                                frameList.Clear();
                                frameCounter = 0;
                                frameList.Add(new FrameObject(f.Timestamp, headX, headY, headZ, spine.X, spine.Y, spine.Z, hipCenterX, hipCenterY, hipCenterZ, floorA, floorB, floorC, floorD));
                            }
                            else
                            {
                                FrameObject newFrame = new FrameObject(f.Timestamp, headX, headY, headZ, spine.X, spine.Y, spine.Z, hipCenterX, hipCenterY, hipCenterZ, floorA, floorB, floorC, floorD);
                                frameList.Add(newFrame);
                                // if classify create object, then change the prevFrame
                                if (svm == null)
                                {
                                    // Change to training data location
                                    svm = new SupportVectorMachine();
                                    svm.buildModel();
                                    //randomForest = new RandomForestEvaluator();
                                    //randomForest.buildModel();

                                }

                                //run algorithm 
                                if (frameList.Count == 3)
                                {

                                    SupportVectorMachine threadModel = new SupportVectorMachine((Accord.MachineLearning.VectorMachines.SupportVectorMachine<Gaussian>)svm.svmModel.Clone(),
                                                                        frameList);
                                    //RandomForest forestClone = randomForest.forest.CloneJson<RandomForest>();
                                    //RandomForestEvaluator threadModel = new RandomForestEvaluator(forestClone, frameList);
                                    // start thread
                                    BackgroundWorker bg = new BackgroundWorker();
                                    bg.DoWork += new DoWorkEventHandler(bg_DoWork);
                                    bg.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bg_RunWorkerCompleted);
                                    bg.RunWorkerAsync(threadModel);
                                }
                            }
                        }
                    }
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
                    colorImage = map;
                    videoBox.Image = map;
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
                skeletonImage.BackColor = SystemColors.Control;
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
                //skeletonImage.DrawSkeleton(body, g);
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += new DoWorkEventHandler(updateUIWorker_DoWork);
                worker.RunWorkerAsync(this.g);
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
            SupportVectorMachine threadObj = e.Argument as SupportVectorMachine;
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
                        bool fall = (bool)fallMessages.Dequeue();
                        Console.WriteLine("-----------");
                        Console.WriteLine(fall);
                        if (fall)
                        {
                            count++;
                        }
                    }
                    Console.WriteLine(count);

                    if (count >= 3)
                    {
                        // Send message when fall is detected
                        Console.WriteLine(DateTime.Now + ": Fall is detected");
                        String message = "Fall Detected! Please check messages for more details.";
                        Bitmap copy = new Bitmap(colorImage);
                        messageSender.sendMessageToAllContact(message, copy);
                    }
                }
            }
        }

        #endregion

        #region update ui worker
        private void updateUIWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Graphics g = e.Argument as Graphics;
            g.DrawSkeleton(body, skeletonImage.Width, skeletonImage.Height);
        }
        #endregion


    }
}

