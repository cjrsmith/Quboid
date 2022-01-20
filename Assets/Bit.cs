using UnityEngine;
using System;

public class Bit : MonoBehaviour
{
    [SerializeField]
    private GameObject sphere;
    [SerializeField]
    private GameObject qubitZero;
    [SerializeField]
    private GameObject qubitOne;
    [SerializeField]
    private GameObject superPosition;
    protected double[][] state = new double[2][];
    protected double[][] matrix = new double[4][];
    protected double thetaDegrees;
    protected double phi;
    protected double prZero;
    protected double prOne;

    //Alter state of qubit according to the gate it hits.
    public void OnCollisionEnter(Collision other)
    {
        if (transform.tag != "collapsed")
        {
            switch (other.transform.tag)
            {
                case "H-Gate":
                    matrix = other.transform.GetComponent<HGate>().getMatrix();
                    state = QMath.matrixMult(matrix, state);
                    //Check values close to 0 and adjust
                    checkCloseToZero(state);
                    calcThetaPhi(state);
                    prZero = QMath.probZero(state);
                    prOne = QMath.probOne(state);
                    spawnState();
                    break;
                case "X-Gate":
                    matrix = other.transform.GetComponent<XGate>().getMatrix();
                    state = QMath.matrixMult(matrix, state);
                    //Check values close to 0 and adjust
                    checkCloseToZero(state);
                    calcThetaPhi(state);
                    prZero = QMath.probZero(state);
                    prOne = QMath.probOne(state);
                    spawnState();
                    break;
                case "Y-Gate":
                    matrix = other.transform.GetComponent<YGate>().getMatrix();
                    state = QMath.matrixMult(matrix, state);
                    //Check values close to 0 and adjust
                    checkCloseToZero(state);
                    calcThetaPhi(state);
                    prZero = QMath.probZero(state);
                    prOne = QMath.probOne(state);
                    spawnState();
                    break;
                case "Z-Gate":
                    matrix = other.transform.GetComponent<ZGate>().getMatrix();
                    state = QMath.matrixMult(matrix, state);
                    //Check values close to 0 and adjust
                    checkCloseToZero(state);
                    calcThetaPhi(state);
                    prZero = QMath.probZero(state);
                    prOne = QMath.probOne(state);
                    spawnState();
                    break;
                case "S-Gate":
                    matrix = other.transform.GetComponent<SGate>().getMatrix();
                    state = QMath.matrixMult(matrix, state);
                    //Check values close to 0 and adjust
                    checkCloseToZero(state);
                    calcThetaPhi(state);
                    prZero = QMath.probZero(state);
                    prOne = QMath.probOne(state);
                    spawnState();
                    break;
                case "T-Gate":
                    matrix = other.transform.GetComponent<TGate>().getMatrix();
                    state = QMath.matrixMult(matrix, state);
                    //Check values close to 0 and adjust
                    checkCloseToZero(state);
                    calcThetaPhi(state);
                    prZero = QMath.probZero(state);
                    prOne = QMath.probOne(state);
                    spawnState();
                    break;
            }

        }
    }
    //Spawn a qubit based on its state
    public void spawnState()
    {
        //Destroy any children inside the sphere before spawning new qubit
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        switch (thetaDegrees)
        {
            case 0:
                //Spawn 0
                GameObject zero = Instantiate(qubitZero, transform.position, Quaternion.identity) as GameObject;
                zero.transform.parent = sphere.transform;
                setColours(zero);
                break;
            case 180:
                //Spawn 1
                GameObject one = Instantiate(qubitOne, transform.position, Quaternion.identity) as GameObject;
                one.transform.parent = sphere.transform;
                setColours(one);
                break;
            default:
                //spawn superpos
                GameObject super = Instantiate(superPosition, transform.position, Quaternion.identity) as GameObject;
                super.transform.parent = sphere.transform;
                setColours(super);
                break;
        }
    }
    //Calculate the phase
    public void phase()
    {
        string str = "";

        if (thetaDegrees == 0)
        {
            foreach (double c in state[0])
            {
                str = str + c.ToString();
            }
            switch (str)
            {
                case "10":
                    phi = 0;
                    break;
                case "01":
                    phi = 90;
                    break;
                case "-10":
                    phi = 180;
                    break;
                case "0-1":
                    phi = 270;
                    break;
                default:
                    double phase = QMath.complexPhi(state[0]);
                    int quadrant = identifyQuandrant(state[0]);
                    switch (quadrant)
                    {
                        case 1:
                            phi = phase;
                            break;
                        case 2:
                            phi = phase + 180;
                            break;
                        case 3:
                            phi = phase + 270;
                            break;
                        case 4:
                            phi = phase + 360;
                            break;
                    }
                    break;
            }
        }
        else if (thetaDegrees == 180)
        {
            foreach (double c in state[1])
            {
                str = str + c.ToString();
            }
            switch (str)
            {
                case "10":
                    phi = 0;
                    break;
                case "01":
                    phi = 90;
                    break;
                case "-10":
                    phi = 180;
                    break;
                case "0-1":
                    phi = 270;
                    break;
                default:
                    double phase = QMath.complexPhi(state[1]);
                    int quadrant = identifyQuandrant(state[1]);
                    switch (quadrant)
                    {
                        case 1:
                            phi = phase;
                            break;
                        case 2:
                            phi = phase + 180;
                            break;
                        case 3:
                            phi = phase + 270;
                            break;
                        case 4:
                            phi = phase + 360;
                            break;
                    }
                    break;
            }
        }
        else
        {
            double[] denominator = { Math.Sin((thetaDegrees / 2) * (Math.PI / 180)), 0 };
            double[] e = QMath.complexDiv(state[1], denominator);
            double r1;
            double r2;
            if (double.IsNaN(Math.Acos(e[0])))
            {
                r1 = 0;
            }
            else
            {
                r1 = Math.Acos(e[0]);
            }
            if (double.IsNaN(Math.Asin(e[1])))
            {
                r2 = 0;
            }
            else
            {
                r2 = Math.Acos(e[0]);
            }
            double alpha = Math.Round(QMath.radToDeg(r1));
            double beta = Math.Round(QMath.radToDeg(r2));
            if (alpha == beta)
            {
                phi = alpha;
            }
            else
            {
                if (alpha < 0)
                {
                    phi = alpha + 360;
                }
                else
                {
                    phi = 360 - alpha;
                }
            }
        }
    }
    //Identify the quandrant on a unit circle.
    public int identifyQuandrant(double[] c1)
    {
        double signC0 = Math.Sign(c1[0]);
        double signC1 = Math.Sign(c1[1]);
        if ((signC0 + signC1) == 2)
        {
            return 1;
        }
        else if ((signC0 + signC1) == -2)
        {
            return 3;
        }
        else if (signC0 == -1)
        {
            return 2;
        }
        else
        {
            return 4;
        }

    }

    public void calcThetaPhi(double[][] state)
    {
        double r0 = QMath.complexMod(state[0]);
        //Set theta
        thetaDegrees = Math.Round(QMath.radToDeg(Math.Acos(r0))) * 2;
        //Calculate the phase and set phi
        phase();
    }

    public int getProbZero()
    {
        return (int)(100 * prZero);
    }
    public int getProbOne()
    {
        return (int)(100 * prOne);
    }
    public double getTheta()
    {
        return thetaDegrees;
    }
    public double getPhi()
    {
        return phi;
    }
    public double[][] getStateArray()
    {
        return state;
    }
    public string getState()
    {
        string stateString = "";
        string c1 = "";
        string c2 = "";
        for (int i = 0; i < state[0].Length; i++)
        {
            c1 += state[0][i];
            if (i == 0)
            {
                c1 += "+";
            }
        }
        for (int i = 0; i < state[1].Length; i++)
        {
            c2 += state[1][i];
            if (i == 0)
            {
                c2 += "+";
            }
        }
        stateString = c1 + " |0> + " + c2 + " |1>";
        return stateString;
    }
    //Set the colours of the qubit according to its state
    public void setColours(GameObject q)
    {
        if (double.IsNaN(phi))
        {
            phi = 360;
        }
        if (double.IsNaN(thetaDegrees))
        {
            thetaDegrees = 180;
        }
        if (thetaDegrees == 0)
        {
            q.transform.GetComponent<Renderer>().material.color = Constants.zeroPhi((float)(phi / 360));
        }
        else if (thetaDegrees == 180)
        {
            q.transform.GetComponent<Renderer>().material.color = Constants.onePhi((float)(phi / 360));
        }
        else
        {
            if (q.transform.childCount > 0)
            {
                q.transform.GetChild(0).GetComponent<Renderer>().material.color = Constants.phiSuper((float)(phi / 360));
                q.transform.GetChild(1).GetComponent<Renderer>().material.color = Constants.thetaSuper((float)(thetaDegrees / 180));
            }
        }
    }

    //Check if a number is close to zero and if so round it to zero. Also if a number is close then round it. 
    //Avoids innacuracies due to Cos and Sin use.
    void checkCloseToZero(double[][] state)
    {
        foreach (double[] c in state)
        {
            for (int i = 0; i < c.Length; i++)
            {
                if (Math.Abs(c[i]) < 0.01)
                {
                    c[i] = 0;
                }
                else if (Math.Abs(Math.Abs(c[i]) - Math.Round(c[i])) < 0.01)
                {
                    c[i] = Math.Round(c[i]);
                }
            }
        }
    }

}
