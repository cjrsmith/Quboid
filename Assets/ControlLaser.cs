using UnityEngine;
public class ControlLaser : Laser
{
    //Abstract class inherited from Laser to allow for different mouse inputs
    protected override void Update()
    {

        if (Input.GetMouseButton(0))
        {
            checkLaser();

        }
        else
        {
            offLaser();
        }
    }
}
