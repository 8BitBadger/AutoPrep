using Godot;
using System;
using EventCallback;

public class Run : Button
{
    public override void _Pressed()
    {
        RunEvent rei = new RunEvent();
        rei.FireEvent();
    }
}
