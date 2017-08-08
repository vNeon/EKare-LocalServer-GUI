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
        private double[][] inputs;
        private int[] outputs;
        private DataTable trainingData = new DataTable("TrainingSet");
        private String filePath;
        private RandomForest forest;

        public RandomForestClassifier(string path)
        {
            this.filePath = path;
        }


        public void buildModel()
        {
            extractInputdata();
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
        
        private void extractInputdata()
        {
            // Read the Excel worksheet into a DataTable    
            //DataTable table = new ExcelReader(fileLocation).GetWorksheet("Sheet1");

            StreamReader oStreamReader = new StreamReader(this.filePath);
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
                            trainingData.Columns.Add(column);
                        }
                    }
                    else
                    {
                        DataRow tableRow = trainingData.NewRow();
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
                        trainingData.Rows.Add(tableRow);

                    }
                }
                rowCount++;
            }
            oStreamReader.Close();
            oStreamReader.Dispose();

            // TODO NEED TO CHANGE TO NUMBER OF FEATURES
            string[] features = new string[570];
            Array.Copy(columnNames, 0, features, 0, 570);

            this.inputs = trainingData.ToJagged<double>(features);
            this.outputs = trainingData.Columns["Class"].ToArray<int>();
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
      

    }
}
