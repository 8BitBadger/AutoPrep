using System;
using Godot;
using EventCallback;

public class StateManager : Node
{
    State currentState;
    public virtual void Init(State startState)
    {
        GD.Print("Base statemanager run Init");
        //Set the starting state here
        currentState = startState;
        currentState.Init();
    }

    public void ChangeState(State newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Init();
    }

    public virtual void Update()
    {
        currentState.Update();
    }

    public virtual void Exit()
    {
        currentState.Exit();
    }
}