using System;
using Accord.Controls;
using Accord.IO;
using Accord.Math;
using Accord.Statistics;
using Accord.MachineLearning.VectorMachines.Learning;
using System.Data;
using System.Collections;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;
using Accord.Statistics.Kernels;
using WindowsFormsApplication1;
using System.Collections.Generic;

public class SVMTest
{
    public string fileLocation;
    public Accord.MachineLearning.VectorMachines.SupportVectorMachine<Gaussian> svmModel { get; set; }
    public double[][] inputs;
    public int[] outputs;
    List<double> data;
    MainFrm _MainFrm;
    FrameObject prevFrame { get; set; }
    FrameObject newFrame { get; set; }

    public SVMTest()
	{
        // Read the Excel worksheet into a DataTable
        fileLocation = Directory.GetCurrentDirectory() + "\\Resources\\Book1.xlsx";
        DataTable table = new Accord.IO.ExcelReader(fileLocation).GetWorksheet("Sheet4");

        // Convert the DataTable to input and output vectors
        this.inputs = table.ToJagged<double>("H_Vel_Y", "DELTA_BOX_W", "DELTA_BOX_H", "DELTA_BOX_D", "HP_Vel_Y");
        this.outputs = table.Columns["Class"].ToArray<int>();
    }

    public SVMTest(string fileLocation)
    {
        // Read the Excel worksheet into a DataTable    
        //DataTable table = new ExcelReader(fileLocation).GetWorksheet("Sheet1");

        StreamReader oStreamReader = new StreamReader(fileLocation);
        DataTable table = new DataTable();
        int rowCount = 0;
        string[] columnNames = null;
        string[] featureVector = null;
        while (!oStreamReader.EndOfStream)
        {
            String row = oStreamReader.ReadLine().Trim();

            if(row.Length > 0)
            {
                featureVector = row.Split(',');
                if (rowCount == 0)
                {
                    columnNames = featureVector;
                    foreach (string columnHeader in columnNames){
                        DataColumn column = new DataColumn(columnHeader.ToUpper(), typeof(string));
                        column.DefaultValue = (float) 0;
                        table.Columns.Add(column);
                    }
                }else
                {
                    DataRow tableRow = table.NewRow();
                    for(int i =0; i<columnNames.Length; i++)
                    {
                        if(featureVector[i] == null)
                        {
                            tableRow[columnNames[i]] = 0;
                        }
                        else
                        {
                            tableRow[columnNames[i]] = featureVector[i];
                        }
                    }
                    table.Rows.Add(tableRow);

                }
            }
            rowCount++;
        }
        oStreamReader.Close();
        oStreamReader.Dispose();

        string[] features = new string[570];
        Array.Copy(columnNames, 0, features,0, 570);

        this.inputs = table.ToJagged<double>(features);
        this.outputs = table.Columns["Class"].ToArray<int>();
        //ScatterplotBox.Show("Fall non fall", inputs, outputs).Hold();
    }

    public SVMTest(Accord.MachineLearning.VectorMachines.SupportVectorMachine<Gaussian> svmModel,
            FrameObject prevFrame,
            FrameObject newFrame,
            MainFrm _MainFrm)
    {
        this.svmModel = svmModel;
        this.prevFrame = prevFrame;
        this.newFrame = newFrame;
        this._MainFrm = _MainFrm;
    }

    public Accord.MachineLearning.VectorMachines.SupportVectorMachine<Gaussian> buildModel()
    {
        // Create a new Sequential Minimal Optimization (SMO) learning 
        // algorithm and estimate the complexity parameter C from data
        var teacher = new SequentialMinimalOptimization<Gaussian>()
        {
            UseComplexityHeuristic = true,
            UseKernelEstimation = true // estimate the kernel from the data
        };

        this.svmModel = teacher.Learn(inputs, outputs);
        return svmModel;
    }

    public int classify(double[][] inputs)
    {
        int[] zeroOneAnswers={0};
        if (svmModel != null)
        {
            bool[] answers = svmModel.Decide(inputs);
            zeroOneAnswers = answers.ToZeroOne();

            //ScatterplotBox.Show("Expected results", inputs, outputs);
            //ScatterplotBox.Show("LinearSVM results", inputs, zeroOneAnswers);
        }
        
        return zeroOneAnswers[0];
    }

    public bool classify()
    {
        bool isFall = classify(this.prevFrame, this.newFrame);
        return isFall;
    }

    public bool classify(string output)
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
        return isFall;
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
