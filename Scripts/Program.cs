using Godot;
using System;
using EventCallback;

public class Program : Button
{
    public override void _Pressed()
    {
        StartProgramEvent rei = new StartProgramEvent();
        rei.FireEvent();
    }
}
