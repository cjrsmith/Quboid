using UnityEngine;
using System;

public class Constants : MonoBehaviour
{

    //return the zero state
    public static double[][] zeroState()
    {
        double[][] zero = new double[2][];
        double[] z0 = { 1, 0 };
        double[] z1 = { 0, 0 };
        zero[0] = z0;
        zero[1] = z1;
        return zero;
    }
    //return the one state
    public static double[][] oneState()
    {
        double[][] one = new double[2][];
        double[] o0 = { 0, 0 };
        double[] o1 = { 1, 0 };
        one[0] = o0;
        one[1] = o1;
        return one;
    }
    //return the plus state
    public static double[][] plusState()
    {
        double[][] plus = new double[2][];
        double[] p0 = { 1 / Math.Sqrt(2), 0 };
        double[] p1 = { 1 / Math.Sqrt(2), 0 };
        plus[0] = p0;
        plus[1] = p1;
        return plus;
    }

    public static Color phiSuper(float value)
    {
        //Colour gradient representing PHI for a super position state. 
        Gradient phiSuper = new Gradient();
        GradientColorKey[] longitude = new GradientColorKey[2];
        GradientAlphaKey[] longitudeA = new GradientAlphaKey[2];
        longitude[0].color = Color.yellow;
        longitude[0].time = 1f; //180 degrees = 100%
        longitude[1].color = Color.red;
        longitude[1].time = 0f; //0 degrees = 0%
        longitudeA[0].alpha = 1f;
        longitudeA[0].time = 1f;
        longitudeA[1].alpha = 1f;
        longitudeA[1].time = 0f;
        phiSuper.SetKeys(longitude, longitudeA);
        return phiSuper.Evaluate(value);
    }
    public static Color thetaSuper(float value)
    {
        //Colour gradient representing THETA for a super position state.
        Gradient thetaSuper = new Gradient();
        GradientColorKey[] latitude = new GradientColorKey[2];
        GradientAlphaKey[] latitudeA = new GradientAlphaKey[2];
        latitude[0].color = Color.green;
        latitude[0].time = 1f; //360 degrees = 100%
        latitude[1].color = Color.magenta;
        latitude[1].time = 0f; //0 degrees = 0%
        latitudeA[0].alpha = 1f;
        latitudeA[0].time = 1f;
        latitudeA[1].alpha = 1f;
        latitudeA[1].time = 0f;
        thetaSuper.SetKeys(latitude, latitudeA);
        return thetaSuper.Evaluate(value);
    }
    public static Color onePhi(float value)
    {
        //Colour gradient representing PHI for the ONE state (Theta not used as this is represented by shape and not colour).
        Gradient onePhi = new Gradient();
        GradientColorKey[] longitudeOne = new GradientColorKey[2];
        GradientAlphaKey[] longitudeOneA = new GradientAlphaKey[2];
        longitudeOne[0].color = Color.magenta;
        longitudeOne[0].time = 1f; //180 degrees = 100%
        longitudeOne[1].color = Color.yellow;
        longitudeOne[1].time = 0f; //0 degrees = 0%
        longitudeOneA[0].alpha = 1f;
        longitudeOneA[0].time = 1f;
        longitudeOneA[1].alpha = 1f;
        longitudeOneA[1].time = 0f;
        onePhi.SetKeys(longitudeOne, longitudeOneA);
        return onePhi.Evaluate(value);
    }
    public static Color zeroPhi(float value)
    {
        //Colour gradient representing PHI for the ZERO state (Theta not used as this is represented by shape and not colour).
        Gradient zeroPhi = new Gradient();
        GradientColorKey[] longitudeZero = new GradientColorKey[2];
        GradientAlphaKey[] longitudeZeroA = new GradientAlphaKey[2];
        longitudeZero[0].color = Color.green;
        longitudeZero[0].time = 1f; //360 degrees = 100%
        longitudeZero[1].color = Color.red;
        longitudeZero[1].time = 0f; //0 degrees = 0%
        longitudeZeroA[0].alpha = 1f;
        longitudeZeroA[0].time = 1f;
        longitudeZeroA[1].alpha = 1f;
        longitudeZeroA[1].time = 0f;
        zeroPhi.SetKeys(longitudeZero, longitudeZeroA);
        return zeroPhi.Evaluate(value);
    }



}
