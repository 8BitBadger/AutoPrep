using System;
using Godot;
using EventCallback;

//The other program classes willl inherit from this one
//It must have a timer array to keep track of what happed where
public class GameEmptyState : State
{
    //Run when the recording starts up
    public override void Init()
    {    
    }
    //Run in the games loop
    public override void Update()
    {
    }
    //Run when the program is unloaded or closed
    public override void Exit()
    {
    }
}