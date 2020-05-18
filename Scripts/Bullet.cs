using Godot;
using System;
using EventCallback;

public class Bullet : Area2D
{
    //The speed of the bullet
    int speed = 700;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        SetAsToplevel(true);
        Position = ((Node2D)GetParent()).GlobalPosition;
        Rotation = ((Node2D)GetParent()).Rotation;
    }
    public override void _PhysicsProcess(float delta)
    {
        Vector2 velocity = new Vector2(Mathf.Cos(Rotation), Mathf.Sin(Rotation)) * speed;
        Position += velocity * delta;
    }
    public void BodyEntered(Node body)
    {
        GD.Print("Bullet hit ", body.Name);
        //Check if it hit a map element first
        if (body.IsInGroup("Map"))
        {
            GD.Print("Collided with map");
            QueueFree();
        }
        //If the collider was not a wall or in the same group as the object that fired it call the hit event
        else if (body.GetGroups() != GetParent().GetGroups())
        {
            GD.Print("Collider was different to the bullet intializer");
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
