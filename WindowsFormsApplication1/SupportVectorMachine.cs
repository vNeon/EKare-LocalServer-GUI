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
using System.Collections.Generic;
using WindowsFormsApplication1;

public class SupportVectorMachine
{
    public string fileLocation;
    public Accord.MachineLearning.VectorMachines.SupportVectorMachine<Gaussian> svmModel { get; set; }
    public double[][] inputs;
    public int[] outputs;
    List<double> data;
    private List<FrameObject> frameList = new List<FrameObject>();

    public SupportVectorMachine()
    {
        // Read the Excel worksheet into a DataTable
        fileLocation = Directory.GetCurrentDirectory() + "\\Resources\\Book1.xlsx";
        //fileLocation = "C:\\Users\\johnn\\Documents\\Visual Studio 2015\\Projects\\FallDetectionService\\FallDetectionService\\Resources\\Book1.xlsx";
        DataTable table = new Accord.IO.ExcelReader(fileLocation).GetWorksheet("Sheet4");

        // Convert the DataTable to input and output vectors
        this.inputs = table.ToJagged<double>("HeadDist_1", "Head_Vel_1", "HipCenterDist_1", "HipCenter_Vel_1", "SpineDist_1", "Spine_Vel_1", "HeadDist_2", "Head_Vel_2", "HipCenterDist_2", "HipCenter_Vel_2", "SpineDist_2", "Spine_Vel_2");
        this.outputs = table.Columns["Class"].ToArray<int>();
    }

    public SupportVectorMachine(string fileLocation)
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

            if (row.Length > 0)
            {
                featureVector = row.Split(',');
                if (rowCount == 0)
                {
                    columnNames = featureVector;
                    foreach (string columnHeader in columnNames)
                    {
                        DataColumn column = new DataColumn(columnHeader.ToUpper(), typeof(string));
                        column.DefaultValue = (float)0;
                        table.Columns.Add(column);
                    }
                }
                else
                {
                    DataRow tableRow = table.NewRow();
                    for (int i = 0; i < columnNames.Length; i++)
                    {
                        if (featureVector[i] == null)
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

        string[] features = new string[14];
        Array.Copy(columnNames, 0, features, 0, 14);

        this.inputs = table.ToJagged<double>(features);
        this.outputs = table.Columns["Class"].ToArray<int>();
        //ScatterplotBox.Show("Fall non fall", inputs, outputs).Hold();
    }

    public SupportVectorMachine(Accord.MachineLearning.VectorMachines.SupportVectorMachine<Gaussian> svmModel,
          List<FrameObject> frameList)
    {
        this.svmModel = null;
        this.svmModel = svmModel;
        this.frameList.Clear();
        this.frameList = frameList;
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
        int[] zeroOneAnswers = { 0 };
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
        double[][] featureVector = frameDiff();

        bool[] answers = { false };
        answers = svmModel.Decide(featureVector);
        // Retunr the classified result
        if (answers[0])
        {
            string str = "";
            foreach (double db in data)
            {
                str += db.ToString() + ",";
            }
            Console.WriteLine(str);
        }
        return answers[0];
    }

    // Calculate the difference between frames
    private double[][] frameDiff()
    {
        data = new List<double>();
        for (int i = 1; i < frameList.Count; i++)
        {
            FrameObject prevFrame = frameList[i - 1];
            FrameObject newFrame = frameList[i];
            long timeDifference = newFrame.Timestamp - prevFrame.Timestamp;

            // Calculate the head difference, 15 frame apart
            double headToFloorDistance = 1.0;
            if (!(newFrame.floorA == 0 && newFrame.floorB == 0 && newFrame.floorC == 0 && newFrame.floorD == 0))
            {
                //Calculate the distance between Head and Floor
                headToFloorDistance = newFrame.HeadX * newFrame.floorA + newFrame.HeadY * newFrame.floorB + newFrame.HeadZ * newFrame.floorC + newFrame.floorD;
                // Scale distance 
                headToFloorDistance *= 100;
            }
            double headDistance = Math.Sqrt(Math.Pow(newFrame.HeadX - prevFrame.HeadX, 2) + Math.Pow(newFrame.HeadY - prevFrame.HeadY, 2) + Math.Pow(newFrame.HeadZ - prevFrame.HeadZ, 2));
            double hipcenterDistance = Math.Sqrt(Math.Pow(newFrame.HipX - prevFrame.HipX, 2) + Math.Pow(newFrame.HipY - prevFrame.HipY, 2) + Math.Pow(newFrame.HipZ - prevFrame.HipZ, 2));
            double spineDistance = Math.Sqrt(Math.Pow(newFrame.SpineX - prevFrame.SpineX, 2) + Math.Pow(newFrame.SpineY - prevFrame.SpineY, 2) + Math.Pow(newFrame.SpineZ - prevFrame.SpineZ, 2));

            data.Add(headDistance * 100);                                   // 3D Head dist in Cm
            data.Add(headDistance * 100000 / timeDifference);
            data.Add(hipcenterDistance * 100);                              // 3D HipCenter dist , Cm
            data.Add(hipcenterDistance * 100000 / timeDifference);
            data.Add(spineDistance * 100);                                  // 3D Spine dist  , Cm
            data.Add(spineDistance * 100000 / timeDifference);
            data.Add(headToFloorDistance);                                  // Head to floor distance in cm
        }
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
