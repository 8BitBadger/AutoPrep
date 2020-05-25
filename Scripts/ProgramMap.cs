using Godot;
using System;
using EventCallback;

public class ProgramMap : TileMap
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        MapUpdateEvent.RegisterListener(UpdateMap);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    private void UpdateMap(MapUpdateEvent muei)
    {
        if (GetCell((int)muei.CollisionPos.x / 32, (int)muei.CollisionPos.y / 32) == 4)
        {
            SetCell((int)muei.CollisionPos.x / 32, (int)muei.CollisionPos.y / 32, 5);
        }
    }

    public override void _ExitTree()
    {
        MapUpdateEvent.UnregisterListener(UpdateMap);
    }
}
