using Accord.Controls;
using Accord.IO;
using Accord.Math;
using Accord.Statistics;
using Accord.MachineLearning.VectorMachines.Learning;
using System.Data;

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
        DataTable table = new Accord.IO.ExcelReader(fileLocation).GetWorksheet("Classification - Yin Yang");

        // Convert the DataTable to input and output vectors
        this.inputs = table.ToJagged<double>("HeadHip", "HeadHead","Time","Bheight","Bwidth");
        this.outputs = table.Columns["Class"].ToArray<int>();

        ScatterplotBox.Show("Yin-Yang", inputs, outputs).Hold();
    }

    public Accord.MachineLearning.VectorMachines.SupportVectorMachine buildModel()
    {
        var teacher = new LinearCoordinateDescent();

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

            ScatterplotBox.Show("Expected results", inputs, outputs);
            ScatterplotBox.Show("LinearSVM results", inputs, zeroOneAnswers);
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
