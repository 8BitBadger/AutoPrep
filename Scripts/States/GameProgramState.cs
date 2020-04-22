using System;
using System.Collections.Generic;
using Godot;
using EventCallback;

//The other program classes willl inherit from this one
//It must have a timer array to keep track of what happed where
public class GameProgramState : IState
{
    //Used to keep track of the time intervals when something changed
    List<ulong> timer = new List<ulong>();
    //When the timer was started
    ulong timerStarted;

    //The timer for the input capturing
    Timer inputTimer = new Timer();

    //Run when the recording starts up
    public void Init()
    {
        GD.Print("Game Programming State Initialized!");
        InputCallbackEvent.RegisterListener(GrabInput);
        //Set the ui state to the programming hud
        SendUIEvent suiei = new SendUIEvent();
        suiei.uiState = UIState.PROGRAMMING_HUD;
        suiei.FireEvent();
    }
    //Run in the games loop
    public void Update()
    {
        // GD.Print(OS.GetTicksUsec());
    }
    //Run when the program is unloaded or closed
    public void Exit()
    {
        GD.Print("Exiting Game Programming State");
        InputCallbackEvent.UnregisterListener(GrabInput);
    }

    private void GrabInput(InputCallbackEvent icei)
    {
        ulong timeStamp = OS.GetTicksUsec();
        GD.Print("Input detected at: " + timeStamp);

    }

}