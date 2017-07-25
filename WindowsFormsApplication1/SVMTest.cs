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

public class SVMTest
{
    public string fileLocation;
    public Accord.MachineLearning.VectorMachines.SupportVectorMachine svmModel;
    public double[][] inputs;
    public int[] outputs;

    public SVMTest()
	{

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

    public Accord.MachineLearning.VectorMachines.SupportVectorMachine buildModel()
    {
        var teacher = new LinearCoordinateDescent();

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

    public void test()
    {
        // Read the Excel worksheet into a DataTable
        DataTable table = new Accord.IO.ExcelReader(@"C:\Users\n\Desktop\examples.xls").GetWorksheet("Classification - Yin Yang");

        // Convert the DataTable to input and output vectors
        double[][] inputs = table.ToJagged<double>("X", "Y");
        int[] outputs = table.Columns["G"].ToArray<int>();

        // Plot the data
        ScatterplotBox.Show("Yin-Yang", inputs, outputs).Hold();


        // Create a L2-regularized L2-loss optimization algorithm for
        // the dual form of the learning problem. This is *exactly* the
        // same method used by LIBLINEAR when specifying -s 1 in the 
        // command line (i.e. L2R_L2LOSS_SVC_DUAL).
        //
        var teacher = new LinearCoordinateDescent();

        // Teach the vector machine
        var svm = teacher.Learn(inputs, outputs);

        // Classify the samples using the model
        bool[] answers = svm.Decide(inputs);

        // Convert to Int32 so we can plot:
        int[] zeroOneAnswers = answers.ToZeroOne();

        // Plot the results
        ScatterplotBox.Show("Expected results", inputs, outputs);
        ScatterplotBox.Show("LinearSVM results", inputs, zeroOneAnswers);

        // Grab the index of multipliers higher than 0
        int[] idx = teacher.Lagrange.Find(x => x > 0);

        // Select the input vectors for those
        double[][] sv = inputs.Get(idx);

        // Plot the support vectors selected by the machine
        ScatterplotBox.Show("Support vectors", sv).Hold();
    }


}
