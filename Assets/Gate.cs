using UnityEngine;

public class Gate : MonoBehaviour
{
    private double[][] matrix = new double[4][];

    public Gate(double a, double b, double c, double d, double e, double f, double g, double h)
    {
        double[] c0 = { a, b };
        double[] c1 = { c, d };
        double[] c2 = { e, f };
        double[] c3 = { g, h };
        matrix[0] = c0;
        matrix[1] = c1;
        matrix[2] = c2;
        matrix[3] = c3;
    }
    //Get the Gate matrix.
    public double[][] getMatrix()
    {
        return matrix;
    }
}
