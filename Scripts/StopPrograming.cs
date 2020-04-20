using Godot;
using System;
using EventCallback;

public class StopPrograming : Button
{
    public override void _Pressed()
    {
        StopProgramEvent spei = new StopProgramEvent();
        spei.FireEvent();
    }
}
