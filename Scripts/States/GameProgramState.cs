using System;
using System.Collections.Generic;
using Godot;
using EventCallback;

public enum InputActions
{
    UP_PRESSED, UP_RELEASED,
    DOWN_PRESSED, DOWN_RELEASED,
    LEFT_PRESSED, LEFT_RELEASED,
    RIGHT_PRESSED, RIGHT_RELEASED,
    LEFT_CLICK_PRESSED, LEFT_CLICK_RELEASED,
    RIGHT_CLICK_PRESSED, RIGHT_CLICK_RELEASED,
    MOUSE_MOVE
};

//The other program classes willl inherit from this one
//It must have a timer array to keep track of what happed where
public class GameProgramState : State
{
    //Used to keep track of the time intervals and input when something changed
    Dictionary<ulong, InputActions> leftInputTimer = new Dictionary<ulong, InputActions>();
    Dictionary<ulong, InputActions> rightInputTimer = new Dictionary<ulong, InputActions>();
    Dictionary<ulong, InputActions> upInputTimer = new Dictionary<ulong, InputActions>();
    Dictionary<ulong, InputActions> downInputTimer = new Dictionary<ulong, InputActions>();
    Dictionary<ulong, InputActions> lmbInputTimer = new Dictionary<ulong, InputActions>();
    Dictionary<ulong, InputActions> rmbInputTimer = new Dictionary<ulong, InputActions>();
    Dictionary<ulong, Vector2> mousePosTimer = new Dictionary<ulong, Vector2>();


    //When the timer was started
    ulong timerStarted;
    //Run when the recording starts up
    public override void Init()
    {
        InputCallbackEvent.RegisterListener(GrabInput);
        MouseInputCallbackEvent.RegisterListener(GrabMouseInput);
        //Set the ui state to the programming hud
        SendUIEvent suiei = new SendUIEvent();
        suiei.uiState = UIState.PROGRAMMING_HUD;
        suiei.FireEvent();
        //Grab the time the program started recording the time for hte user input
        timerStarted = OS.GetTicksMsec();

        
    }
    //Run in the games loop
    public override void Update()
    {
    }
    //Run when the program is unloaded or closed
    public override void Exit()
    {
        //Call program event and pass along the movement data to the run state
        SendProgramEvent pei = new SendProgramEvent();
        pei.leftInputTimer = leftInputTimer;
        pei.rightInputTimer = rightInputTimer;
        pei.upInputTimer = upInputTimer;
        pei.downInputTimer = downInputTimer;
        pei.lmbInputTimer = lmbInputTimer;
        pei.rmbInputTimer = rmbInputTimer;
        pei.mousePosTimer = mousePosTimer;
        pei.FireEvent();

        InputCallbackEvent.UnregisterListener(GrabInput);
        MouseInputCallbackEvent.UnregisterListener(GrabMouseInput);
    }

    private void GrabInput(InputCallbackEvent icei)
    {
        //The timestamp is worked out by getting the current tick and subtracting the start of the session tick amount
        ulong timeStamp = OS.GetTicksMsec() - timerStarted;
        if (icei.leftPressed) leftInputTimer.Add(timeStamp, InputActions.LEFT_PRESSED);
        if (icei.leftRelease) leftInputTimer.Add(timeStamp, InputActions.LEFT_RELEASED);
        if (icei.rightPressed) rightInputTimer.Add(timeStamp, InputActions.RIGHT_PRESSED);
        if (icei.rightRelease) rightInputTimer.Add(timeStamp, InputActions.RIGHT_RELEASED);
        if (icei.upPressed) upInputTimer.Add(timeStamp, InputActions.UP_PRESSED);
        if (icei.upRelease) upInputTimer.Add(timeStamp, InputActions.UP_RELEASED);
        if (icei.downPressed) downInputTimer.Add(timeStamp, InputActions.DOWN_PRESSED);
        if (icei.downRelease) downInputTimer.Add(timeStamp, InputActions.DOWN_RELEASED);
        if (icei.lmbClickPressed) lmbInputTimer.Add(timeStamp, InputActions.LEFT_CLICK_PRESSED);
        if (icei.lmbClickRelease) lmbInputTimer.Add(timeStamp, InputActions.LEFT_CLICK_RELEASED);
        if (icei.rmbClickPressed) rmbInputTimer.Add(timeStamp, InputActions.RIGHT_CLICK_PRESSED);
        if (icei.rmbClickRelease) rmbInputTimer.Add(timeStamp, InputActions.RIGHT_CLICK_RELEASED);
    }
    private void GrabMouseInput(MouseInputCallbackEvent micei)
    {
        //The timestamp is worked out by getting the current tick and subtracting the start of the session tick amount
        ulong timeStamp = OS.GetTicksMsec() - timerStarted;
        //mousePosTimer.Add(timeStamp, micei.mousePos);
    }
}