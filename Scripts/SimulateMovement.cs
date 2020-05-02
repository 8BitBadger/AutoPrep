using Godot;
using System;

public class SimulateMovement : KinematicBody2D
{


    public override void _PhysicsProcess(float delta)
    {
        /*
        if (up) inputVelocity.y = -1;
        else if (down) inputVelocity.y = 1;
        else inputVelocity.y = 0;
        if (left) inputVelocity.x = -1;
        else if (right) inputVelocity.x = 1;
        else inputVelocity.x = 0;

        velocity = inputVelocity.Normalized() * speed;
        //Look in the directio of the mouses global position
        LookAt(globalMousePos);
        //Move and slide the rigidbaody 2d
        MoveAndSlide(velocity);
        */
    }
}
