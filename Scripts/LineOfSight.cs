using Godot;
using System;
using EventCallback;

public class LineOfSight : Area2D
{
    //The target for the raycast
    Node target;
    //If set to true then it will raycast the target to keep track of lone of sight tracking the target 
    bool trackTarget = false, lookAtTarget = false, lookingAtTarget = false;
    //The interval that the raytracing should be fired at
    float lastTrackedTime = 0;
    //Turn speed of the turret
    float turnSpeed = 0.07f;

    public override void _PhysicsProcess(float delta)
    {
        //If the we have a targe tand the timer is above .6 of a second we can check
        if (trackTarget)
        {
            if (lastTrackedTime >= .6f)
            {
                TrackTarget();
                lastTrackedTime = 0;
            }
            //Count up to the next target track method call
            lastTrackedTime += delta;
        }
        //If the target is in line of sight then look at it
        if (lookAtTarget)
        {
            LookAtTarget();
        }
    }
    private void LookAtTarget()
    {
        //Look in the direction of the targets global position
        float angleToTarget = GetNode<Node2D>("../Gun").GetAngleTo(((Node2D)target).Position);
        if (Mathf.Abs(angleToTarget) < turnSpeed)
        {
            GetNode<Node2D>("../Gun").Rotation += angleToTarget;
        }
        else
        {
            if (angleToTarget > 0) GetNode<Node2D>("../Gun").Rotation += turnSpeed;
            if (angleToTarget < 0) GetNode<Node2D>("../Gun").Rotation -= turnSpeed;
        }
        //If the gun is looking at the player se set it to true to enable our gun to fire
        if (angleToTarget == 0)
        {
            lookingAtTarget = true;
        }
        else
        {
            lookingAtTarget = false;
        }
    }
    public void BodyEntered(Node body)
    {
        if (!body.IsInGroup("Player")) return;
        trackTarget = true;
        target = body;
    }
    public void BodyExited(Node body)
    {
        if (!body.IsInGroup("Player")) return;
        lookAtTarget = false;
        trackTarget = false;
        target = null;
    }
    private void TrackTarget()
    {
        if (target == null) return;
        Physics2DDirectSpaceState worldState = GetWorld2d().DirectSpaceState;
        //Get the raycast hits and store them in a dictionary
        Godot.Collections.Dictionary hits = worldState.IntersectRay(this.GlobalPosition, ((KinematicBody2D)target).GlobalPosition);
        if (hits.Count > 0)
        {
            if (hits.Contains("collider"))
            {
                Node2D col = (Node2D)hits["collider"];
                if (col.IsInGroup("Player"))
                {
                    if(lookingAtTarget) ((Gun)GetNode<Node2D>("../Gun/Nozzle")).Fire();
                    lookAtTarget = true;
                }
                else
                {
                    lookAtTarget = false;
                }
            }
        }
    }
}
