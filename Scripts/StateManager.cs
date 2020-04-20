using System;
using Godot;
using EventCallback;

public abstract class StateManager
{
    State currentState;
    public abstract void Init(State startState)
    {
        //Set the starting state here
        ChangeState(startState);
    }

    public void ChangeState(State newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Init();
    }

    public void Update()
    {
        currentState.Update();
    }
}