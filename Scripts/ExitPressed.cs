using Godot;
using System;
using EventCallback;

public class ExitPressed : Button
{
    // Called when the node enters the scene tree for the first time.
    public override void _Pressed()
    {
        GetTree().Quit();
    }
}
