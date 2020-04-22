using Godot;
using System;

public class GameWaitState : IState
{
    //Run when the state starts up
    public void Init()
    {
        GD.Print("Game Wait State Initialized!");
    }
    //Run in the games loop
    public void Update()
    {

    }
    //Run when the program is unloaded or closed
    public void Exit()
    {
        GD.Print("Exiting Game Wait State");
    }
}
