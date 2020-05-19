using Godot;
using System;
using EventCallback;

public class Laser : Node2D
{
    int maxDistance = 1000;
    Line2D laserBeam;
    Particles2D BeamHitParticles;
    KinematicBody2D hitBox;

    PackedScene bulletScene = new PackedScene();
    public override void _Ready()
    {
        laserBeam = GetNode<Line2D>("LaserBeam");
        BeamHitParticles = GetNode<Particles2D>("BeamHitParticles");
        hitBox = GetNode<KinematicBody2D>("../../../LaserTurret");
    }
    public override void _PhysicsProcess(float delta)
    {
        //GD.Print("Parent Name = " + GetParent().Name);
        //GD.Print("Parent Rotation = " + ((Node2D)GetParent()).GlobalPosition);
        //GD.Print("this Name = " + this.Name);
        //GD.Print("this Rotation = " + GlobalPosition);

        Physics2DDirectSpaceState worldState = GetWorld2d().DirectSpaceState;
        //Get the raycast hits and store them in a dictionary
        //Godot.Collections.Dictionary hits = worldState.IntersectRay(GlobalPosition, (((Node2D)GetParent()).GlobalPosition + ((Node2D)GetParent()).Transform.x * maxDistance));
        Godot.Collections.Dictionary hits = worldState.IntersectRay(GlobalPosition, GlobalPosition +Transform.x * maxDistance);
        //If there are no hits then we return out of the function
        if (hits.Count > 0)
        {
            GD.Print("Hit Detected");
            Vector2 hitPos = (Vector2)hits["position"];
            laserBeam.SetPointPosition(1, new Vector2(hitPos.x, 0));
            BeamHitParticles.Emitting = true;
            BeamHitParticles.Position = new Vector2(hitPos.x, 0);            
        }
        else
        {
            BeamHitParticles.Emitting = false;
           //laserBeam.SetPointPosition(1, new Vector2(maxDistance, 0));
            laserBeam.SetPointPosition(1, GlobalPosition +Transform.x * maxDistance);

        }
    }
    public void Fire()
    {

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
