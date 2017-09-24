using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.MachineLearning.DecisionTrees;
using Accord.Math.Random;
using System.Data;
using System.IO;
using Accord.Math;
using Accord.Math.Optimization.Losses;

namespace WindowsFormsApplication1
{
    class RandomForestClassifier
    {
        public double[][] inputs { get; set; }
        public int[] outputs { get; set; }
        private DataTable trainingData = new DataTable("TrainingSet");
        private String filePath = "";
        public RandomForest forest { get; set; }
        List<double> data;
        public List<FrameObject> frameList { get; set; }

        public RandomForestClassifier()
        {
        }

        public RandomForestClassifier(RandomForest forest,
           List<FrameObject> frameList)
        {
            this.forest = null;
            this.forest = forest;
            this.frameList.Clear();
            this.frameList = frameList;
        }

        public RandomForestClassifier(List<FrameObject> frameList)
        {
            this.frameList = frameList;
        }

        public void buildModel()
        {
            extractInputdata();
            var attributes = DecisionVariable.FromData(inputs);
            // Now, let's create the forest learning algorithm
            var teacher = new RandomForestLearning(attributes)
            {
                NumberOfTrees = 2,
                SampleRatio = 1.0
            };

            // Finally, learn a random forest from data
            this.forest = teacher.Learn(inputs, outputs);
        }
        
        private void extractInputdata()
        {
            // Read the Excel worksheet into a DataTable    
            //DataTable table = new ExcelReader(fileLocation).GetWorksheet("Sheet1");
            // Read the Excel worksheet into a DataTable
            string fileLocation = Directory.GetCurrentDirectory() + "\\Resources\\Book1.xlsx";
            //fileLocation = "C:\\Users\\johnn\\Documents\\Visual Studio 2015\\Projects\\FallDetectionService\\FallDetectionService\\Resources\\Book1.xlsx";
            DataTable table = new Accord.IO.ExcelReader(fileLocation).GetWorksheet("Sheet4");

            // Convert the DataTable to input and output vectors
            this.inputs = table.ToJagged<double>("HeadDist_1", "Head_Vel_1", "HipCenterDist_1", "HipCenter_Vel_1", "SpineDist_1", "Spine_Vel_1", "HeadDist_2", "Head_Vel_2", "HipCenterDist_2", "HipCenter_Vel_2", "SpineDist_2", "Spine_Vel_2");
            this.outputs = table.Columns["Class"].ToArray<int>();

            //StreamReader oStreamReader = new StreamReader(this.filePath);
            //int rowCount = 0;
            //string[] columnNames = null;
            //string[] featureVector = null;
            //while (!oStreamReader.EndOfStream)
            //{
            //    String row = oStreamReader.ReadLine().Trim();

            //    if (row.Length > 0)
            //    {
            //        featureVector = row.Split(',');
            //        if (rowCount == 0)
            //        {
            //            columnNames = featureVector;
            //            foreach (string columnHeader in columnNames)
            //            {
            //                DataColumn column = new DataColumn(columnHeader.ToUpper(), typeof(string));
            //                column.DefaultValue = (float)0;
            //                trainingData.Columns.Add(column);
            //            }
            //        }
            //        else
            //        {
            //            DataRow tableRow = trainingData.NewRow();
            //            for (int i = 0; i < columnNames.Length; i++)
            //            {
            //                if (featureVector[i] == null)
            //                {
            //                    tableRow[columnNames[i]] = 0;
            //                }
            //                else
            //                {
            //                    tableRow[columnNames[i]] = featureVector[i];
            //                }
            //            }
            //            trainingData.Rows.Add(tableRow);

            //        }
            //    }
            //    rowCount++;
            //}
            //oStreamReader.Close();
            //oStreamReader.Dispose();

            //// TODO NEED TO CHANGE TO NUMBER OF FEATURES
            //string[] features = new string[67];
            //Array.Copy(columnNames, 0, features, 0, 67);

            //this.inputs = trainingData.ToJagged<double>(features);
            //this.outputs = trainingData.Columns["Class"].ToArray<int>();
        }
        
        // Learn model
        public void learnData()
        {
            var attributes = DecisionVariable.FromData(inputs);
            // Now, let's create the forest learning algorithm
            var teacher = new RandomForestLearning(attributes)
            {
                NumberOfTrees = 1,
                SampleRatio = 1.0
            };

            // Finally, learn a random forest from data
            this.forest = teacher.Learn(inputs, outputs);
        }

        public int classify(double[][] inputs)
        {
            // We can estimate class labels using
            int[] predicted = forest.Decide(inputs);

            // And the classification error (0) can be computed as 
            double error = new ZeroOneLoss(outputs).Loss(forest.Decide(inputs));

            //Count the number of 1s and 0s
            int numFall = 0;
            int numNonFall = 0;
            foreach (int output  in predicted)
            {
                if (output == 1)
                {
                    numFall++;
                }else
                {
                    numNonFall++;
                }
            }
            return numFall >= numNonFall ? 1 : 0;
        }

     
        public int classify()
        {
            double[][] featureVector = frameDiff();

            int[] answers = { 0 };
            answers = forest.Decide(featureVector);
            // Retunr the classified result
            if (answers[0]==1)
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

    }
}
