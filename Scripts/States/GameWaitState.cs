using Godot;
using System;
using EventCallback;

public class GameWaitState : State
{
    //Run when the state starts up
    public override void Init()
    {
                    //Set the ui state to the wait hud state
            SendUIEvent suiei = new SendUIEvent();
            suiei.uiState = UIState.WAIT_HUD;
            suiei.FireEvent();
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
