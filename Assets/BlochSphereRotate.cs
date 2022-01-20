using UnityEngine;

public class BlochSphereRotate : MonoBehaviour
{
    [SerializeField]
    private GameObject origin;
    [SerializeField]
    private GameObject axis;
    //Rotate the arrow according to theta and phi
    public void setState(double th, double ph)
    {
        Vector3 rotationToAdd = new Vector3(0, (float)ph * -1, (float)th * -1);
        axis.transform.Rotate(rotationToAdd);
    }
    //Reset the arrow to its original position
    public void resetState()
    {
        axis.transform.localPosition = new Vector3(0, 0, 0); ;
        axis.transform.localRotation = new Quaternion(0, 0, 0, 1); ;
    }
}
