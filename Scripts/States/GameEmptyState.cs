using System;
using Godot;
using EventCallback;

//The other program classes willl inherit from this one
//It must have a timer array to keep track of what happed where
public class GameEmptyState : IState
{

    //Run when the recording starts up
    public void Init()
    {
        
    }
    //Run in the games loop
    public void Update()
    {

    }
    //Run when the program is unloaded or closed
    public void Exit()
    {

    }

}