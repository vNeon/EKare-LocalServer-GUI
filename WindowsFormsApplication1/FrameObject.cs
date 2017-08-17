using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class FrameObject
    {
        public long Timestamp { get; set; }
        public double HeadX { get; set; }
        public double HeadY { get; set; }
        public double HeadZ { get; set; }
        public double BoxW { get; set; }
        public double BoxH { get; set; }
        public double BoxD { get; set; }
        public double SpineX { get; set; }
        public double SpineY { get; set; }
        public double SpineZ { get; set; }
        public double HipX { get; set; }
        public double HipY { get; set; }
        public double HipZ { get; set; }

        public FrameObject(long ts, double HeadX, double HeadY, double HeadZ,
                            double BoxW, double BoxH, double BoxD,
                            double SpineX, double SpineY, double SpineZ,
                            double HipX, double HipY, double HipZ)
        {
            Timestamp = ts;
            this.HeadX = HeadX;
            this.HeadY = HeadY;
            this.HeadZ = HeadZ;
            this.BoxW = BoxW;
            this.BoxH = BoxH;
            this.BoxD = BoxD;
            this.SpineX = SpineX;
            this.SpineY = SpineY;
            this.SpineZ = SpineZ;
            this.HipX = HipX;
            this.HipY = HipY;
            this.HipZ = HipZ;
        }
    }
}
