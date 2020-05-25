using Godot;
using System;
using EventCallback;

public class Bullet : Area2D
{
    //The speed of the bullet
    int speed = 700;
    Vector2 velocity;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        SetAsToplevel(true);
        Position = ((Node2D)GetParent()).GlobalPosition;
        Rotation = ((Node2D)GetParent()).Rotation;
    }
    public override void _PhysicsProcess(float delta)
    {
        velocity = new Vector2(Mathf.Cos(Rotation), Mathf.Sin(Rotation)) * speed;
        Position += velocity * delta;
    }
    public void BodyEntered(Node body)
    {
        //Check if it hit a map element first
        if (body.IsInGroup("Map"))
        {
            MapUpdateEvent muei = new MapUpdateEvent();
            //Add one more step of velocity to the colliders position so that we make sure the bullet is inside the tile before we
            //return the bullets position to the map for refferencing the tile that was hit
            velocity = new Vector2(Mathf.Cos(Rotation), Mathf.Sin(Rotation)) * speed / 2;
            muei.CollisionPos = (Position += velocity * .01f);
            muei.FireEvent();
            QueueFree();
        }
        //If the collider was not a wall or in the same group as the object that fired it call the hit event
        //else if (body.GetGroups() != GetParent().GetGroups())
        else if (body != GetParent().GetParent())
        {
            //Fire of the hit event
            HitEvent hei = new HitEvent();
            hei.target = (Node2D)body;
            hei.attacker = (Node2D)GetParent();
            hei.damage = 100;
            hei.FireEvent();
            QueueFree();
        }
    }
}
