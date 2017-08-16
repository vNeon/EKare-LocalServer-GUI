using Accord.Controls;
using Accord.IO;
using Accord.Math;
using Accord.Statistics;
using Accord.MachineLearning.VectorMachines.Learning;
using System.Data;
using Microsoft.Kinect;
using WindowsFormsApplication1;
using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Forms;

public class SVMTest
{
    public string fileLocation;
    public Accord.MachineLearning.VectorMachines.SupportVectorMachine svmModel { get; set; }
    public double[][] inputs;
    public int[] outputs;
    FrameObject prevFrame { get; set; }
    FrameObject newFrame { get; set; }
    List<double> data;
    MainFrm _MainFrm;

    public SVMTest()
	{

    }

    public SVMTest(Accord.MachineLearning.VectorMachines.SupportVectorMachine svmModel,
                    FrameObject prevFrame,
                    FrameObject newFrame)
    {
        this.svmModel = svmModel;
        this.prevFrame = prevFrame;
        this.newFrame = newFrame;
    }

    public SVMTest(Accord.MachineLearning.VectorMachines.SupportVectorMachine svmModel,
                FrameObject prevFrame,
                FrameObject newFrame,
                MainFrm _MainFrm)
    {
        this.svmModel = svmModel;
        this.prevFrame = prevFrame;
        this.newFrame = newFrame;
        this._MainFrm = _MainFrm;
    }

    public SVMTest(string fileLocation)
    {
        // Read the Excel worksheet into a DataTable
        DataTable table = new Accord.IO.ExcelReader(fileLocation).GetWorksheet("Sheet4");

        // Convert the DataTable to input and output vectors
        this.inputs = table.ToJagged<double>("H_Vel_Y", "DELTA_BOX_W", "DELTA_BOX_H", "DELTA_BOX_D", "HP_Vel_Y");
        this.outputs = table.Columns["Class"].ToArray<int>();
    }

    public Accord.MachineLearning.VectorMachines.SupportVectorMachine buildModel()
    {
        var teacher = new LinearCoordinateDescent();

        this.svmModel = teacher.Learn(inputs, outputs);
        return svmModel;
    }

    public void classify()
    {
        bool isFall = classify(this.prevFrame, this.newFrame);
        if (isFall && _MainFrm != null)
        {
            _MainFrm.AppendToBox("Fall Detected!, Data:  " + this.ToString() + "\n");
        }
        else if (isFall)
        {
            Console.WriteLine("Fall Detected!, Data: " + this.ToString());
        }

    }

    public bool classify(double[][] inputs)
    {
        bool[] answers = { false };
        if (svmModel != null)
        {
            answers = svmModel.Decide(inputs);
        }
        
        return answers[0];
    }

    public bool classify(FrameObject prevFrame, FrameObject newFrame)
    {
        double[][] featureVector = frameDiff(prevFrame, newFrame);

        bool[] answers = { false };
        answers = svmModel.Decide(featureVector);
        return answers[0];
    }

    public double[][] frameDiff(FrameObject prevFrame, FrameObject newFrame)
    {
        data = new List<double>();
        long timeDifference = newFrame.Timestamp - prevFrame.Timestamp;
        //data.Add(newFrame.HeadY);
        //data.Add((newFrame.HeadY - prevFrame.HeadY) / (timeDifference));
        //data.Add((newFrame.BoxW - prevFrame.BoxW) / (timeDifference));
        //data.Add((newFrame.BoxH - prevFrame.BoxH) / (timeDifference));
        //data.Add((newFrame.BoxD - prevFrame.BoxD) / (timeDifference));

        //data.Add(newFrame.HeadX);
        //data.Add(newFrame.HeadY);
        //data.Add(newFrame.HeadZ);
        //data.Add((newFrame.HeadX - prevFrameObject.HeadX) / (timeDifference));
        data.Add((newFrame.HeadY - prevFrame.HeadY) / (timeDifference));
        //data.Add((newFrame.HeadZ - prevFrameObject.HeadZ) / (timeDifference));
        //data.Add(newFrame.BoxW);
        //data.Add(newFrame.BoxH);
        //data.Add(newFrame.BoxD);
        data.Add((newFrame.BoxW - prevFrame.BoxW) / (timeDifference));
        data.Add((newFrame.BoxH - prevFrame.BoxH) / (timeDifference));
        data.Add((newFrame.BoxD - prevFrame.BoxD) / (timeDifference));
        //data.Add(newFrame.SpineX);
        //data.Add(newFrame.SpineY);
        //data.Add(newFrame.SpineZ);
        //data.Add(newFrame.HipX);
        //data.Add(newFrame.HipY);
        //data.Add(newFrame.HipZ);
        //data.Add((newFrame.HipX - prevFrameObject.HipX) / (timeDifference));
        data.Add((newFrame.HipY - prevFrame.HipY) / (timeDifference));
        //data.Add((newFrame.HipZ - prevFrameObject.HipZ) / (timeDifference));

        double[][] input = new double[1][];
        input[0] = data.ToArray() as double[];

        return input;
    }

    override public string ToString()
    {
        if (data != null)
        {
            string str = "";
            foreach (double db in data)
            {
                str += db.ToString() + ",";
            }
            return str;
        }
        else
        {
            return "NULL";
        }
    }
}
