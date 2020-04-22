using Godot;
using System;
using EventCallback;

public class GameWaitState : IState
{
    //Run when the state starts up
    public void Init()
    {
        GD.Print("Game Wait State Initialized!");
                    //Set the ui state to the wait hud state
            SendUIEvent suiei = new SendUIEvent();
            suiei.uiState = UIState.WAIT_HUD;
            suiei.FireEvent();
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
