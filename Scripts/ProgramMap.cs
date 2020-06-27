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
        //I we hit a wall we just return out of the tile check
        if(GetCell((int)muei.CollisionPos.x / 32, (int)muei.CollisionPos.y / 32) == 3) return;
        //Convert the position from the collision passed in to intiger to use in the tile map
        int tileX = (int)muei.CollisionPos.x / 32;
        int tileY = (int)muei.CollisionPos.y / 32;

//Run through all the surrounding tiles in the collision 
        for (int x = tileX -1; x < tileX + 1; x++)
        {
            for (int y = tileY -1; y < tileY + 1; y++)
            {
                if (GetCell(x, y) == 4)
                {
                    SetCell(x, y, 5);
                }
            }
        }

    }

    public override void _ExitTree()
    {
        MapUpdateEvent.UnregisterListener(UpdateMap);
    }
}
