using Godot;
using System;
using EventCallback;

public class Laser : Node2D
{
    bool canFire = false;
    int maxDistance = 1000;
    Line2D laserBeam;
    Particles2D BeamHitParticles;
    KinematicBody2D hitBox;

    PackedScene bulletScene = new PackedScene();
    public override void _Ready()
    {
        SetAsToplevel(true);
        laserBeam = GetNode<Line2D>("LaserBeam");
        BeamHitParticles = GetNode<Particles2D>("BeamHitParticles");
        hitBox = GetNode<KinematicBody2D>("../../../LaserTurret");
    }
    public override void _PhysicsProcess(float delta)
    {
        if (!canFire) return;
        Physics2DDirectSpaceState worldState = GetWorld2d().DirectSpaceState;
        //Get the raycast hits and store them in a dictionary

        //This works, why?!?!???!?!?!?!!
        //Why does it not work on the child object, is it becuse the child object is not centered
        Godot.Collections.Dictionary hits = worldState.IntersectRay(((Node2D)GetParent()).GlobalPosition, ((Node2D)GetParent()).GlobalPosition + ((Node2D)GetParent()).Transform.x * maxDistance, new Godot.Collections.Array { GetNode<KinematicBody2D>("../../../LaserTurret") });
        //If there are no hits then we return out of the function
        if (hits.Count > 0)
        {
            Vector2 hitPos = (Vector2)hits["position"];
            //Offset the start position of hte laser so that it looks like it is originating at the nozzle of the barrel
            laserBeam.SetPointPosition(0, ((Node2D)GetParent().GetParent()).Position);
            laserBeam.SetPointPosition(1, hitPos);

            BeamHitParticles.Emitting = true;
            BeamHitParticles.Position = hitPos;
            //Fire of the hit event
            HitEvent hei = new HitEvent();
            hei.target = (Node2D)hits["collider"];
            hei.attacker = (Node2D)GetParent();
            hei.damage = 100;
            hei.FireEvent();
        }
        else
        {
            BeamHitParticles.Emitting = false;
            //Works do not delete!!!!!!!!!!!!!!!
            //========================================
            laserBeam.SetPointPosition(0, new Vector2(16, 0));
            laserBeam.SetPointPosition(1, Position + Transform.x * maxDistance);
            //========================================
        }
    }
    public void Fire()
    {
        canFire = true;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
