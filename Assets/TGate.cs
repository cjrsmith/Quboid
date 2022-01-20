using System;

public class TGate : Gate
{
    public TGate() : base(1, 0, 0, 0, 0, 0, Math.Cos((Math.PI / 180) * 45), Math.Sin((Math.PI / 180) * 45))
    {

    }
}
