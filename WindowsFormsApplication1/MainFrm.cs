﻿using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Kinect;
using Coding4Fun.Kinect.WinForm;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;
namespace WindowsFormsApplication1
{
    public partial class MainFrm : Form
    {

        private NotificationSender notifier = new NotificationSender();
        private MessageSender messageSender = new MessageSender();

        private String username = String.Empty;
        private KinectSensor kinect;
        private const float MaxDepthDistance = 4000;
        private const float MinDepthDistance = 850;
        private const float MaxDepthDistanceOffset = MaxDepthDistance - MinDepthDistance;

        //HEAD postions:
        private SkeletonPoint prevH;
        private float preHX = -1;
        private float preHY = -1;
        //private float preHZ = -1;
        private long prevTs = -1;

        private int counter=150;
        private int counterInputCount = 0;
        //SVM Model Object
        public SVMTest svm;


        // Count down end time
        DateTime endCounter;
        private double[][] trainingDataset = new double[10][];
       
        public MainFrm(String user)
        {
            username = user;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            notifier.SendNotification();
        }

        private void sendMessageBtn_Click(object sender, EventArgs e)
        {
            String message = messageTb.Text;
            messageSender.sendMessageToAllContact(message);
        }

        /// <summary>
        ///  Get user data from the firebase database
        /// </summary>
        private void GetDataFromFirebase()

        {
            string uri = "https://myfirstapplication-5ad99.firebaseio.com/users/6b4rvGyIgMgQMo1XQBVKglI5k7l1.json/?auth=rngcgjOb25J68o1JW5XUEFigUbO86kNQmKxN4IB5";
            FirebaseRequest fq = new FirebaseRequest( uri, httpMethod.GET);
            fq.makeRequest();
            String res = fq.executeGetRequest();
            tbOutput.AppendText(res);
        }



        private void Form1_Load(object sender, EventArgs e)
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
                //kinect.DepthStream.Enable();
                kinect.SkeletonStream.Enable();
                //kinect.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);


            }
        }

        //private void startKinectSensors()
        //{
        //    if (!kinect.IsRunning)
        //    {
        //        kinect.Start();
        //        tbOutput.AppendText("Started Kinect!\n");
        //    }

        //    Enabling Color stream
        //    kinect.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
        //    kinect.ColorFrameReady += Kinect_ColorFrameReady;

        //    Enabling Depth stream
        //    kinect.DepthStream.Enable();
        //    kinect.DepthStream.Range = DepthRange.Default;
        //    kinect.DepthFrameReady += Kinect_DepthFrameReady;

        //    Enabling Skeleton tracking
        //    kinect.SkeletonStream.Enable();
        //    kinect.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
        //    kinect.SkeletonFrameReady += Kinect_SkeletonFrameReady;
        //}

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton1.Checked)
            {
                if (!kinect.IsRunning)
                {
                    kinect.Start();

                    tbOutput.AppendText("Started Kinect!\n");
                }
                kinect.DepthStream.Disable();
                //  kinect.SkeletonStream.Disable();
                kinect.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                kinect.ColorFrameReady += Kinect_ColorFrameReady;
            }
            else if (radioButton2.Checked)
            {
                if (!kinect.IsRunning)
                {
                    kinect.Start();
                    tbOutput.AppendText("Started Kinect!\n");
                }
                kinect.ColorStream.Disable();
                // kinect.SkeletonStream.Disable();
                kinect.DepthStream.Enable();
                kinect.DepthStream.Range = DepthRange.Default;
                kinect.DepthFrameReady += Kinect_DepthFrameReady;

            }
            else
            {
                kinect.Stop();
                tbOutput.AppendText("Stopped Kinect!\n");
                lblConnectionID.Text = "-";
                videoBox.Image = null;
            }
        }

        //Turn on the skeleton tracking 
        private void skeletonCB_CheckedChanged(object sender, EventArgs e)
        {
            if (kinect.IsRunning)
            {
                kinect.SkeletonStream.Enable();
                //kinect.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
                kinect.SkeletonFrameReady += Kinect_SkeletonFrameReady;
            }
        }   

        // Get the skeletal coordinates
        private void Kinect_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame f = e.OpenSkeletonFrame())
            {
                counter++;
                if (f == null)
                {
                    return;
                }
                var skeletons = new Skeleton[f.SkeletonArrayLength];
                f.CopySkeletonDataTo(skeletons);

                //Find the first person to track
                var trackedPerson = skeletons.FirstOrDefault(s => s.TrackingState == SkeletonTrackingState.Tracked);

                long timeNow = Convert.ToInt64( f.Timestamp.ToString());

                if (trackedPerson != null)
                {
                    SkeletonPoint positionHead = trackedPerson.Joints[JointType.Head].Position;
                    SkeletonPoint positionHip = trackedPerson.Joints[JointType.HipCenter].Position;
                    headXlbl.Text = positionHead.X.ToString();
                    headYlbl.Text = positionHead.Y.ToString();
                    headZlbl.Text = positionHead.Z.ToString();
                    SkeletonPoint shoulderLeft = trackedPerson.Joints[JointType.ShoulderLeft].Position;
                    SkeletonPoint shoulderRight = trackedPerson.Joints[JointType.ShoulderRight].Position;
                    SkeletonPoint hipCenter = trackedPerson.Joints[JointType.HipCenter].Position;
                    if (preHX ==-1)
                    {
                    }
                    else
                    {
                        Console.WriteLine(counter);
                        if (counter >= 150) {
                            
                            double headHipDiff = Math.Sqrt(Math.Pow(positionHead.X - positionHip.X, 2) + Math.Pow(positionHead.Y - positionHip.Y, 2)
                                                    + Math.Pow(positionHead.Y - positionHip.Y, 2));
                            double headDiff = Math.Sqrt(Math.Pow(positionHead.X - prevH.X, 2) + Math.Pow(positionHead.Y - prevH.Y, 2)
                                                    + Math.Pow(positionHead.Y - prevH.Y, 2));

                            long timeDiff = timeNow - prevTs;

                            double boundingBoxWidth = Math.Abs(shoulderLeft.X - shoulderRight.X) + 0.6;
                            double boundindBoxHeight = Math.Abs(positionHead.Y - hipCenter.Y) + 1.2;
                            double[] inputArray = { headHipDiff, headDiff, timeDiff, boundindBoxHeight, boundingBoxWidth };
                            double[][] in1 = new double[1][];
                            in1[0] = inputArray;
                            int res=svm.classify(in1);
                            if (res == 1)
                            {
                                messageSender.sendMessageToAllContact("Fall dtected " + GlobalValues.user.name);
                            }

                            //Console.WriteLine("INPUT: " +counterInputCount);
                            ////for (int i =0; i<5; i++)
                            ////{
                            ////    Console.Write(inputArray[i] + " -- ");
                            ////}
                            ////Console.WriteLine();
                            ////counterInputCount++;
                            ////counter = 0;
                         }
                    }

                    // update previous values
                    prevTs = timeNow;
                    prevH = positionHead;
                    preHX = positionHead.X;
                    CoordinateMapper mapper = new CoordinateMapper(kinect);
                    var colorPoint = mapper.MapSkeletonPointToColorPoint(positionHead, ColorImageFormat.InfraredResolution640x480Fps30);

                }
            }
        }


        private void Kinect_DepthFrameReady(object sender, DepthImageFrameReadyEventArgs e)
        {
            using (DepthImageFrame f = e.OpenDepthImageFrame())
            {
                if (f != null)
                {
                    videoBox.Image = CreateDepthBitMap(f);
                }
            }
        }


        private void Kinect_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using ( ColorImageFrame f = e.OpenColorImageFrame())
            {
                if (f != null)
                {
                    videoBox.Image = CreateBitMapFromSensor(f);
                }
            }
        }

        //private Bitmap CreateSkeletonTrackingMap(SkeletonFrame f)
        //{
        //    //list of skeletons
        //    var skeletons = new Skeleton[f.SkeletonArrayLength];
        //    f.CopySkeletonDataTo(skeletons);

        //    //Find the first person to track
        //    var trackedPerson = skeletons.FirstOrDefault(s => s.TrackingState == SkeletonTrackingState.Tracked);

        //    if (trackedPerson != null)
        //    {
        //        SkeletonPoint position = trackedPerson.Joints[JointType.Head].Position;
        //        CoordinateMapper mapper = new CoordinateMapper(kinect);
        //        var colorPoint = mapper.MapSkeletonPointToColorPoint(position, ColorImageFormat.InfraredResolution640x480Fps30);

        //    }


        //}

        private Bitmap CreateBitMapFromSensor(ColorImageFrame frame)
        {
            var pixelData = new byte[frame.PixelDataLength];
            frame.CopyPixelDataTo(pixelData);

            var stride = frame.Width * frame.BytesPerPixel;
            var bmpFrame = new Bitmap(frame.Width, frame.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            var bmpData = bmpFrame.LockBits(new Rectangle(0, 0, frame.Width, frame.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, bmpFrame.PixelFormat);
            System.Runtime.InteropServices.Marshal.Copy(pixelData, 0, bmpData.Scan0, frame.PixelDataLength);

            bmpFrame.UnlockBits(bmpData);
            return bmpFrame;
        }

        private Bitmap CreateDepthBitMap(DepthImageFrame f)
        {
            // raw data from kinect with the depth for every pixel
            short[] depthData = new short[f.PixelDataLength];
            f.CopyPixelDataTo(depthData);

            IntPtr pdata = Marshal.AllocHGlobal(sizeof(short) * f.PixelDataLength);
            try
            {
                f.CopyPixelDataTo(pdata, f.PixelDataLength);
            }
            finally
            {
                if (pdata != IntPtr.Zero) Marshal.FreeHGlobal(pdata);
            }

            Console.WriteLine(depthData.Length);
            //for(int i =0; i <f.PixelDataLength; i++)
            //{
            //    Console.WriteLine(depthData[i]);
            //}

           // Console.WriteLine(pdata.ToInt32().ToString());

            //We want to build a bitmap, hence need an array of bytes
            // The size of the byte array is the width * height *4, 4 since we are doing rgba
            Byte[] colorPixels = new byte[f.Height * f.Width * 4];
            int stride = f.Width * 4;

            const int blueIndex= 0;
            const int greenIndex = 1;
            const int redIndex = 2;

            for(int depthIndex =0, colorIndex =0; depthIndex < depthData.Length && colorIndex < colorPixels.Length; depthIndex ++, colorIndex += 4)
            {
                int player = colorPixels[depthIndex] & DepthImageFrame.PlayerIndexBitmask;

                int depth = depthData[depthIndex] >> DepthImageFrame.PlayerIndexBitmaskWidth;

                if (depth <= 900)
                {
                    colorPixels[colorIndex + blueIndex] = 255;
                    colorPixels[colorIndex + greenIndex] = 0;
                    colorPixels[colorIndex + redIndex] = 0;
                }
                else if (depth > 900 & depth < 2000)
                {
                    colorPixels[colorIndex + blueIndex] = 0;
                    colorPixels[colorIndex + greenIndex] = 255;
                    colorPixels[colorIndex + redIndex] = 0;
                }
                else if (depth >= 2000)
                {
                    colorPixels[colorIndex + blueIndex] = 0;
                    colorPixels[colorIndex + greenIndex] = 0;
                    colorPixels[colorIndex + redIndex] = 255;
                }

                ////Monocrhomatic greyscale
                //byte intensity = CalculateIntensityFromDepth(depth);

                //colorPixels[colorIndex + blueIndex] = intensity;
                //colorPixels[colorIndex + greenIndex] = intensity;
                //colorPixels[colorIndex + redIndex] = intensity;

            }

            BitmapSource btSource = BitmapSource.Create(f.Width, f.Height, 96, 96, PixelFormats.Bgr32, null, colorPixels, stride);
            Bitmap bitmap;
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(btSource));
                enc.Save(outStream);
                bitmap = new System.Drawing.Bitmap(outStream);
            }
            return bitmap;
 

        }


        private void KinectSensors_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            this.lblStatus.Text = kinect.Status.ToString();
        }

     
        private void DisplayRGB()
        {
            kinect.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
            kinect.ColorFrameReady += Kinect_ColorFrameReady;
        }

        public static byte CalculateIntensityFromDepth(int distance )
        {

            return (byte)(255 - (255 * Math.Max(distance - MinDepthDistance, 0) / (MaxDepthDistanceOffset)));

        }
        private void DisplayDepth()
        {

        }
        
        private void DisplaySkeletal()
        {

        }

        private void DetectFall(SkeletonPoint head, SkeletonPoint hip)
        {
            if(Math.Abs(head.Y - hip.Y) <= 0.1 || (preHY -head.Y) >= 0.5  )
            {
                notifier.SendNotification();
                tbOutput.AppendText("Fall detected");
            }else
            {
                tbOutput.AppendText(Math.Abs(head.Y - hip.Y).ToString() + " ---- " + Math.Abs(preHY - head.Y).ToString());
            }


        }
        

        private void storeDataTest()
        {
            
        }

        private void SVMBtn_Click(object sender, EventArgs e)
        {
            svm = new SVMTest(@"C:\Users\johnn\Desktop\trainingdata.xlsx");
            svm.buildModel();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (svm != null)
            {
                svm.classify(new double[1][]);
            }
        }

        /// <summary>
        /// 
        /// Start record after 5 second
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void recorBtn_Click(object sender, EventArgs e)
        {
            DateTime startTime = DateTime.UtcNow;
            var seconds = 5; //countdown time
            var start = DateTime.UtcNow; // Use UtcNow instead of Now
            endCounter = start.AddSeconds(seconds); //endTime is a member, not a local variable
            timer1.Enabled = true;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan remainingTime = endCounter - DateTime.UtcNow;
            if (remainingTime < TimeSpan.Zero)
            {
                counterLbl.Text="Started Recording!";
                //timer1.Enabled = false;
            }
            else
            {
                counterLbl.Text = remainingTime.ToString("ss");
            }
        }
    }
}
