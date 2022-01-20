using UnityEngine;
public class MeasureLaser : Laser
{
    //Abstract class inherited from Laser to allow for different mouse inputs
    protected override void Update()
    {

        if (Input.GetMouseButton(1))
        {
            checkLaser();

        }
        else
        {
            offLaser();
        }
    }
}