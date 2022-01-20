using UnityEngine;

public class Hologram : Bit
{
    [SerializeField]
    private GameObject thetaText;
    [SerializeField]
    private GameObject phiText;
    [SerializeField]
    private GameObject stateText;

    //Set the state of the qubit hologram
    public void setState(double[][] newState)
    {
        state = newState;
        calcThetaPhi(state);
        spawnState();
        displayStats();
    }
    //Display the stats of the qubit 
    void displayStats()
    {
        thetaText.transform.GetComponent<TextMesh>().text = "Theta: " + thetaDegrees + "\u00B0";
        phiText.transform.GetComponent<TextMesh>().text = "Phi: " + phi + "\u00B0";
        stateText.transform.GetComponent<TextMesh>().text = getState();
    }

}
