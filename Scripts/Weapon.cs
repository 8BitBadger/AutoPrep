using Godot;
using System;
using EventCallback;

public class Weapon : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    PackedScene bulletScene = new PackedScene();
    Node bullet;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        InputCallbackEvent.RegisterListener(GetMouseButtonInput);
        bulletScene = ResourceLoader.Load("res://Scenes/BulletProgram.tscn") as PackedScene;
    }

    private void GetMouseButtonInput(InputCallbackEvent icei)
    {
        if (icei.lmbClickPressed)
        {
            //Instance bullet, set the rotation and start position
            bullet = bulletScene.Instance();
            this.GetParent().AddChild(bullet);
        }
    }
}
