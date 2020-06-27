using Godot;
using System;
using EventCallback;

public class LaserLineOfSight : Area2D
{
    //The target for the raycast
    Node target;
    //If set to true then it will raycast the target to keep track of lone of sight tracking the target 
    bool targetInRange = false, targetInLineOfSight = false, lookingAtTarget = false;
    //The interval that the raytracing should be fired at
    float lastLOSCheckedTime = 0;
    //Turn speed of the turret
    float turnSpeed = Mathf.Deg2Rad(1);//0.1f;

    public override void _PhysicsProcess(float delta)
    {
        //If the we have a targe tand the timer is above .6 of a second we can check
        if (targetInRange)
        {
            if (lastLOSCheckedTime >= .6f)
            {
                lineOfSightCheck();
                lastLOSCheckedTime = 0;
            }
            //Count up to the next target track method call
            lastLOSCheckedTime += delta;
        }
        //If the target is in line of sight then look at it
        if (targetInLineOfSight)
        {//If the player is in sight the lazer starts shooting and coontinues to turn towards the player
            ((Laser)GetNode<Node2D>("../Gun/Laser")).Fire();
            FaceTarget();
        }
    }
    public void BodyEntered(Node body)
    {
        if (!body.IsInGroup("Player")) return;
        targetInRange = true;
        target = body;
    }
    public void BodyExited(Node body)
    {
        if (!body.IsInGroup("Player")) return;
        targetInLineOfSight = false;
        targetInRange = false;
        target = null;
    }
    private void lineOfSightCheck()
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
                    targetInLineOfSight = true;
                }
            }
        }
    }
    private void FaceTarget()
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
        /*
        //If the gun is looking at the player se set it to true to enable our gun to fire
        GD.Print(GetParent().Name + "'s angle is = " + Mathf.Rad2Deg(angleToTarget));
        //If the target is within 1 to -1 degrees of the turret is fires the bullet
        if (Mathf.Rad2Deg(angleToTarget) <= 1 && Mathf.Rad2Deg(angleToTarget) >= -1)
        {
            ((Gun)GetNode<Node2D>("../Gun/Lazer")).Fire();
        }
        */
    }
}
