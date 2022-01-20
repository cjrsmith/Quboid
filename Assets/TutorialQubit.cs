using System;
using UnityEngine;
public class TutorialQubit : Bit
{
    [SerializeField]
    private GameObject moveToPoint;
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject bloch;
    [SerializeField]
    private GameObject sceneCamera;
    [SerializeField]
    private int count;
    [SerializeField]
    private int startState;
    [SerializeField]
    private Canvas finalCanvas;
    private bool end = false;

    // Start is called before the first frame update
    void Start()
    {
        switch (startState)
        {
            case 0:
                state = Constants.zeroState();
                break;
            case 1:
                state = Constants.oneState();
                break;
            case 2:
                state = Constants.plusState();
                break;
        }
        initialSpawn(state);
        bloch.transform.GetComponent<BlochSphereRotate>().setState(getTheta(), getPhi());
    }
    private void OnTriggerEnter(Collider other)
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
                bloch.transform.GetComponent<BlochSphereRotate>().resetState();
                bloch.transform.GetComponent<BlochSphereRotate>().setState(getTheta(), getPhi());
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
                bloch.transform.GetComponent<BlochSphereRotate>().resetState();
                bloch.transform.GetComponent<BlochSphereRotate>().setState(getTheta(), getPhi());
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
                bloch.transform.GetComponent<BlochSphereRotate>().resetState();
                bloch.transform.GetComponent<BlochSphereRotate>().setState(getTheta(), getPhi());
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
                bloch.transform.GetComponent<BlochSphereRotate>().resetState();
                bloch.transform.GetComponent<BlochSphereRotate>().setState(getTheta(), getPhi());
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
                bloch.transform.GetComponent<BlochSphereRotate>().resetState();
                bloch.transform.GetComponent<BlochSphereRotate>().setState(getTheta(), getPhi());
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
                bloch.transform.GetComponent<BlochSphereRotate>().resetState();
                bloch.transform.GetComponent<BlochSphereRotate>().setState(getTheta(), getPhi());
                break;
        }
    }
    public void initialSpawn(double[][] firstState)
    {
        calcThetaPhi(firstState);
        prZero = QMath.probZero(firstState);
        prOne = QMath.probOne(firstState);
        spawnState();
    }
    void Update()
    {
        if (sceneCamera.transform.GetComponent<Tutorial>().getCount() == count)
        {
            //Deactivate current canvas
            sceneCamera.transform.GetComponent<Tutorial>().deactivateCurrentCanvas();
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, moveToPoint.transform.position, step);
            if (transform.position == moveToPoint.transform.position && !end)
            {
                sceneCamera.transform.GetComponent<Tutorial>().endScene();
                //Activate continuecanvas
                sceneCamera.transform.GetComponent<Tutorial>().activateContinueCanvas();
            }
            else if (transform.position == moveToPoint.transform.position && end)
            {
                sceneCamera.transform.GetComponent<Tutorial>().endScene();
                //Activate final canvas
                finalCanvas.gameObject.GetComponentInChildren<Canvas>().enabled = true;
            }
        }
    }
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
    public void endTutorial()
    {
        end = true;
    }

}
