using System;
using Godot;
using EventCallback;

public class StateManager : Node
{
    Node currentState, oldState;
    public virtual void Init(Node startState)
    {
        //Set the starting state here
        currentState = startState;
        //We add the node as a child to the state manager
        AddChild(currentState);
        //We grab the state script from the state node and call the init method from it
        ((State)currentState).Init();
    }

    public void ChangeState(Node newState)
    {
        //Error checking if the new state node has a State script attached
        if (!newState.HasMethod("Init")) GD.PrintErr("State Manager - Loaded state might not have State script attached, no Init method found");
        //We set the current state tot the old state
        oldState = currentState;
        //We run the currents states exit function to close any memory usage and other exit actions
        ((State)currentState).Exit();
        //We set the current state to the new state
        currentState = newState;
        //We remove the old state
        RemoveChild(oldState);
        //We add the current state as a child of the state managers scene
        AddChild(currentState);
        //We run the Init for the current state
        ((State)currentState).Init();
    }

    public virtual void Update()
    {
        //We call the update method from the current state
        ((State)currentState).Update();
    }

    public virtual void Exit()
    {
        //The exit function is called from the current states attached script
        ((State)currentState).Exit();
    }
}