using Godot;
using System;

public class Main : Node2D
{
    SystemState systemState;
    GameState gameState;
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public void ChangeSystemState(SystemState newState)
    {
        systemState = newState;
    }

    public void ChangeGameState(GameState newState)
    {
        gameState = newState;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
