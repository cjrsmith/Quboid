using UnityEngine;

public class Platform : MonoBehaviour
{
    private double[][] state = new double[2][];
    [SerializeField]
    protected GameObject portal;
    private bool reached = false;
    private bool firstTime = true;
    private bool measure;
    private bool capture;

    //Set the state and requirements
    public Platform(double a, double b, double c, double d, bool mea, bool cap)
    {
        double[] z0 = { a, b };
        double[] z1 = { c, d };
        state[0] = z0;
        state[1] = z1;
        measure = mea;
        capture = cap;
    }
    //Return the state
    public double[][] getState()
    {
        return state;
    }
    //If in contact with player for first time then send the required info to the player.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (firstTime)
            {
                reached = true;
                collision.transform.GetChild(0).GetChild(0).GetComponent<CameraRayCast>().requiredState(gameObject, state, measure, capture);
                firstTime = false;
            }
        }
    }
    public bool platformReached()
    {
        return reached;
    }
    //Update portal to allow teleportation.
    public void updatePortal()
    {
        portal.transform.GetComponent<Portal>().activate();
    }
    //Call the portals win method.
    public void updatePortalWin()
    {
        portal.transform.GetComponent<Portal>().win();
    }

}
