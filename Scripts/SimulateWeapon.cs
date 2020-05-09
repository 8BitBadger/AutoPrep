using Godot;
using System;
using EventCallback;

public class SimulateWeapon : Node2D
{
    PackedScene bulletScene = new PackedScene();
    Node bullet;
        public override void _Ready()
    {
        bulletScene = ResourceLoader.Load("res://Scenes/Bullet.tscn") as PackedScene;
    }

     public void FireBullet()
    {
            //Instance bullet, set the rotation and start position
            bullet = bulletScene.Instance();
            this.GetParent().AddChild(bullet);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
