using System;
using Godot;
using EventCallback;

public abstract class StateManager
{
    IState currentState;
    public void Init(IState startState)
    {
        //Set the starting state here
        currentState = startState;
        currentState.Init();
    }

    public void ChangeState(IState newState)
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