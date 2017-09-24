using Microsoft.Kinect;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WindowsFormsApplication1
{
    /**
     * Class DrawExtensions
     * This class draws using the infomation obtained from the Kinect sensors
     * Draws RGB, Depth and skeletaltraking images.
     */
    public static class DrawExtensions
    {

        public static Bitmap CreateBitMapFromSensor(this ColorImageFrame frame)
        {
            var pixelData = new byte[frame.PixelDataLength];
            frame.CopyPixelDataTo(pixelData);

            var stride = frame.Width * frame.BytesPerPixel;
            var bmpFrame = new Bitmap(frame.Width, frame.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            var bmpData = bmpFrame.LockBits(new System.Drawing.Rectangle(0, 0, frame.Width, frame.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, bmpFrame.PixelFormat);
            System.Runtime.InteropServices.Marshal.Copy(pixelData, 0, bmpData.Scan0, frame.PixelDataLength);

            bmpFrame.UnlockBits(bmpData);
            return bmpFrame;
        }


        public static Bitmap CreateDepthBitMap(this DepthImageFrame f)
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

            //We want to build a bitmap, hence need an array of bytes
            // The size of the byte array is the width * height *4, 4 since we are doing rgba
            Byte[] colorPixels = new byte[f.Height * f.Width * 4];
            int stride = f.Width * 4;

            const int blueIndex = 0;
            const int greenIndex = 1;
            const int redIndex = 2;

            for (int depthIndex = 0, colorIndex = 0; depthIndex < depthData.Length && colorIndex < colorPixels.Length; depthIndex++, colorIndex += 4)
            {
                int player = colorPixels[depthIndex] & DepthImageFrame.PlayerIndexBitmask;

                int depth = depthData[depthIndex] >> DepthImageFrame.PlayerIndexBitmaskWidth;

                // RGB
                //if (depth <= 900)
                //{
                //    colorPixels[colorIndex + blueIndex] = 255;
                //    colorPixels[colorIndex + greenIndex] = 0;
                //    colorPixels[colorIndex + redIndex] = 0;
                //}
                //else if (depth > 900 & depth < 2000)
                //{
                //    colorPixels[colorIndex + blueIndex] = 0;
                //    colorPixels[colorIndex + greenIndex] = 255;
                //    colorPixels[colorIndex + redIndex] = 0;
                //}
                //else if (depth >= 2000)
                //{
                //    colorPixels[colorIndex + blueIndex] = 0;
                //    colorPixels[colorIndex + greenIndex] = 0;
                //    colorPixels[colorIndex + redIndex] = 255;
                //}

                //Monocrhomatic greyscale
                byte intensity = CalculateIntensityFromDepth(depth);

                colorPixels[colorIndex + blueIndex] = intensity;
                colorPixels[colorIndex + greenIndex] = intensity;
                colorPixels[colorIndex + redIndex] = intensity;

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

        public static byte CalculateIntensityFromDepth(int distance)
        {

            return (byte)( (255 * Math.Max(distance - 850, 0) / (4000)));

        }

        #region Skeleton

        public static Joint ScaleTo(this Joint joint, double width, double height, float skeletonMaxX, float skeletonMaxY)
        {

            joint.Position = new SkeletonPoint
            {
                X = Scale(width, skeletonMaxX, joint.Position.X),
                Y = Scale(height, skeletonMaxY, -joint.Position.Y),
                Z = joint.Position.Z
            };

            return joint;
        }

        public static Joint ScaleTo(this Joint joint, double width, double height)
        {
            return ScaleTo(joint, width, height, 1.0f, 1.0f);
        }

        private static float Scale(double maxPixel, double maxSkeleton, float position)
        {
            float value = (float)((((maxPixel / maxSkeleton) / 2) * position) + (maxPixel / 2));

            if (value > maxPixel)
            {
                return (float)maxPixel;
            }

            if (value < 0)
            {
                return 0;
            }

            return value;
        }

        #endregion

        #region Draw

        public static void DrawSkeleton(this Graphics g, Skeleton skeleton,int width, int height)
        {
            if (skeleton == null) return;

            foreach (Joint joint in skeleton.Joints)
            {
                g.DrawPoint(joint, width, height);
            }

            g.DrawLine(skeleton.Joints[JointType.Head], skeleton.Joints[JointType.ShoulderCenter], width,height);
            g.DrawLine(skeleton.Joints[JointType.ShoulderCenter], skeleton.Joints[JointType.ShoulderRight], width, height);
            g.DrawLine(skeleton.Joints[JointType.ShoulderCenter], skeleton.Joints[JointType.ShoulderLeft], width, height);
            g.DrawLine(skeleton.Joints[JointType.ShoulderCenter], skeleton.Joints[JointType.Spine], width, height);
            g.DrawLine(skeleton.Joints[JointType.ShoulderLeft], skeleton.Joints[JointType.ElbowLeft], width, height);
            g.DrawLine(skeleton.Joints[JointType.ShoulderRight], skeleton.Joints[JointType.ElbowRight], width, height);
            g.DrawLine(skeleton.Joints[JointType.ElbowLeft], skeleton.Joints[JointType.WristLeft], width, height);
            g.DrawLine(skeleton.Joints[JointType.ElbowRight], skeleton.Joints[JointType.WristRight], width, height);
            g.DrawLine(skeleton.Joints[JointType.WristLeft], skeleton.Joints[JointType.HandLeft], width, height);
            g.DrawLine(skeleton.Joints[JointType.WristRight], skeleton.Joints[JointType.HandRight],width, height);
            g.DrawLine(skeleton.Joints[JointType.Spine], skeleton.Joints[JointType.HipCenter], width, height);
            g.DrawLine(skeleton.Joints[JointType.HipCenter], skeleton.Joints[JointType.HipLeft], width, height);
            g.DrawLine(skeleton.Joints[JointType.HipCenter], skeleton.Joints[JointType.HipRight], width, height);
            g.DrawLine(skeleton.Joints[JointType.HipLeft], skeleton.Joints[JointType.KneeLeft], width, height);
            g.DrawLine(skeleton.Joints[JointType.HipRight], skeleton.Joints[JointType.KneeRight], width, height);
            g.DrawLine(skeleton.Joints[JointType.KneeLeft], skeleton.Joints[JointType.AnkleLeft], width, height);
            g.DrawLine(skeleton.Joints[JointType.KneeRight], skeleton.Joints[JointType.AnkleRight], width, height);
            g.DrawLine(skeleton.Joints[JointType.AnkleLeft], skeleton.Joints[JointType.FootLeft], width, height);
            g.DrawLine(skeleton.Joints[JointType.AnkleRight], skeleton.Joints[JointType.FootRight], width, height);
        }

        public static void DrawPoint(this Graphics g, Joint joint, int width, int height)
        {
            if (joint.TrackingState == JointTrackingState.NotTracked) return;

            joint = joint.ScaleTo(width, height);

            g.DrawEllipse(Pens.CadetBlue, joint.Position.X, joint.Position.Y, 5, 5);
        }

        public static void DrawLine(this Graphics g, Joint first, Joint second,int width, int height)
        {
            if (first.TrackingState == JointTrackingState.NotTracked || second.TrackingState == JointTrackingState.NotTracked) return;

            first = first.ScaleTo(width, height);
            second = second.ScaleTo(width, height);
            System.Drawing.Pen bluePen = new System.Drawing.Pen(System.Drawing.Color.CadetBlue, 3);
            g.DrawLine(bluePen, first.Position.X,first.Position.Y, second.Position.X,second.Position.Y);

        }
        #endregion

    }

}
