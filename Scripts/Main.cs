using Godot;
using System;

public class Main : Node2D
{
    //The state manager for the games systems
    StateManager systemStateManager = new StateManager();
    //States for the games systems
    State systemMenuState;
    State systemHUDState;
    State systweWinState;
    State systemLoseState;

    //The state manager for the games recording mechanic
    StateManager gameStateManager;
    //States for hte games recording mechanic
    State gameProgramState;
    State gameRunState;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {//Init the system state manager to the menu state
        systemStateManager.Init(systemMenuState);

    }
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        systemStateManager.Update();
    }
}