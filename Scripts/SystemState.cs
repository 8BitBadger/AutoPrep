using Godot;
using System;
using EventCallback;

public enum SystemStates
{
    LOAD,
    MENU,
    GAME,
    WIN,
    LOSE
};

public class SystemState
{
    SystemStates currentState;
    public void SetState(SystemStates state)
    {
        currentState = state;
    }
}
