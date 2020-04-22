using System;
using System.Collections.Generic;
using Godot;
using EventCallback;

//The other program classes willl inherit from this one
//It must have a timer array to keep track of what happed where
public class GameProgramState : IState
{
    //Used to keep track of the time intervals when something changed
    List<float> timer = new List<float>();

    //Run when the recording starts up
    public void Init()
    {
        GD.Print("Game Programming State Initialized!");
    }
    //Run in the games loop
    public void Update()
    {

    }
    //Run when the program is unloaded or closed
    public void Exit()
    {
        GD.Print("Exiting Game Programming State");
    }

}