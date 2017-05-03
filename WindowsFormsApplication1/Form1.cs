﻿using System;
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

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        private KinectSensor kinect;
        private const float MaxDepthDistance = 4000;
        private const float MinDepthDistance = 850;
        private const float MaxDepthDistanceOffset = MaxDepthDistance - MinDepthDistance;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String message = textBox1.Text;
            String str;
            try
            {
                string applicationID = "AAAAxHOyOu8:APA91bFN66xR3tpsOQC1OywO6s1Wv_8aq5iXNZdDnp1aog9LVjoySszWuHtvjZaEch0rQr5o3T3HoXgsKjbbZijFIJqy8rQVunQEAopAcbfP4dAAEgUKbb7woALRahEU7398wLwembnk";

                string senderId = "843754650351";

                //string deviceId = "ch_G60NPga4:APA9............T_LH8up40Ghi-J";

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    to = "/topics/all",
                    notification = new
                    {
                        body = message,
                        title = "Message:",
                        sound = "Enabled"
                    }
                };
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                str = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }

            tbOutput.Text = str;
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
            }
        }

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
                kinect.SkeletonStream.Disable();
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
                kinect.SkeletonStream.Disable();
                kinect.DepthStream.Enable();
                //kinect.DepthStream.Range = DepthRange.Near;
                kinect.DepthFrameReady += Kinect_DepthFrameReady;
                
            }
            else if (radioButton3.Checked)
            {
                if (!kinect.IsRunning)
                {
                    kinect.Start();
                    tbOutput.AppendText("Started Kinect!\n");
                }
                kinect.ColorStream.Disable();
                kinect.DepthStream.Disable();
                kinect.SkeletonStream.Enable();
                kinect.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
                kinect.SkeletonFrameReady += Kinect_SkeletonFrameReady;
            }
            else
            {
                kinect.Stop();
                tbOutput.AppendText("Stopped Kinect!\n");
                lblConnectionID.Text = "-";
                videoBox.Image = null;
            }
        }


        private void Kinect_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame f = e.OpenSkeletonFrame())
            {
                if (f != null)
                {
                    //videoBox.Image = CreateSkeletonTrackingMap(f);
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
    }
}

