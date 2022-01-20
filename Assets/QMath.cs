using UnityEngine;
using System;

public class QMath : MonoBehaviour
{
    //Multiply two complex numbers.
    public static double[] complexMult(double[] c1, double[] c2)
    {
        double[] result = new double[2];
        result[0] = (c1[0] * c2[0]) - (c1[1] * c2[1]);
        result[1] = (c2[0] * c1[1]) + (c2[1] * c1[0]);
        return result;
    }
    //Divide two complex number.
    public static double[] complexDiv(double[] c1, double[] c2)
    {
        double[] result = new double[2];
        double denominator = Math.Pow(c2[0], 2) + Math.Pow(c2[1], 2);
        result[0] = ((c1[0] * c2[0]) + (c1[1] * c2[1])) / denominator;
        result[1] = ((c1[1] * c2[0]) - (c1[0] * c2[1])) / denominator;
        return result;
    }
    //Calculate the natural logarithm of a complex number.
    public static double[] complexLog(double[] c1)
    {
        double[] result = new double[2];
        result[0] = 0.5 * Math.Log(Math.Sqrt(c1[0] + Math.Sqrt(c1[1])));
        result[1] = radToDeg(Math.Atan((c1[1] / c1[0])));
        return result;
    }
    //Add two complex numbers.
    public static double[] complexAdd(double[] c1, double[] c2)
    {
        double[] result = new double[2];
        result[0] = (c1[0] + c2[0]);
        result[1] = (c1[1] + c2[1]);
        return result;
    }
    //Subtract two complex numbers.
    public static double[] complexSub(double[] c1, double[] c2)
    {
        double[] result = new double[2];
        result[0] = (c1[0] - c2[0]);
        result[1] = (c1[1] - c2[1]);
        return result;
    }
    //Get the length of a complex number.
    public static double complexMod(double[] c1)
    {
        double result = Math.Sqrt(Math.Pow(c1[0], 2) + Math.Pow(c1[1], 2));
        return result;
    }

    //Probability of collapsing to 0
    public static double probZero(double[][] state)
    {
        return Math.Pow(complexMod(state[0]), 2);
    }
    //Probability of collapsing to 1
    public static double probOne(double[][] state)
    {
        return Math.Pow(complexMod(state[1]), 2);
    }
    //Calculate phi for a given complex number
    public static double complexPhi(double[] c1)
    {
        if (c1[0] == 0 && c1[1] == 0)
        {
            return 0;
        }
        else
        {
            return radToDeg(Math.Atan((c1[1] / c1[0])));
        }
    }
    //Matrix multiply
    public static double[][] matrixMult(double[][] matrix, double[][] state)
    {
        double[][] result = new double[2][];
        result[0] = complexAdd(complexMult(matrix[0], state[0]), complexMult(matrix[2], state[1]));
        result[1] = complexAdd(complexMult(matrix[1], state[0]), complexMult(matrix[3], state[1]));
        return result;
    }
    //Convert radians to degrees
    public static double radToDeg(double rad)
    {
        return (rad * (180 / Math.PI));
    }

}
