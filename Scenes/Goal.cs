using Godot;
using System;
using EventCallback;
public class Goal : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public void BodyEntered(Node body)
    {
        if (body.IsInGroup("Player"))
        {
            //Fire the win event if the area was collided with
            WinEvent wei = new WinEvent();
            wei.FireEvent();
        }
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
