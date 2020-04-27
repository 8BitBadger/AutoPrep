using Godot;
using System;
using System.Collections.Generic;
using EventCallback;

public class GameRunState : State
{
    Dictionary<ulong, InputActions> leftInputTimer = new Dictionary<ulong, InputActions>();
    Dictionary<ulong, InputActions> rightInputTimer = new Dictionary<ulong, InputActions>();
    Dictionary<ulong, InputActions> upInputTimer = new Dictionary<ulong, InputActions>();
    Dictionary<ulong, InputActions> downInputTimer = new Dictionary<ulong, InputActions>();
    Dictionary<ulong, InputActions> lmbInputTimer = new Dictionary<ulong, InputActions>();
    Dictionary<ulong, InputActions> rmbInputTimer = new Dictionary<ulong, InputActions>();
    Dictionary<ulong, Vector2> mousePosTimer = new Dictionary<ulong, Vector2>();
    //Run when the state starts up
    public override void Init()
    {
        //We use the event manager to grab the values, we do not call the fire event
        //becuase we dont want to store any new values just get the current ones
        GetProgramEvent gpei = new GetProgramEvent();
        gpei.FireEvent();
        leftInputTimer = gpei.leftInputTimer;
        rightInputTimer = gpei.rightInputTimer;
        upInputTimer = gpei.upInputTimer;
        downInputTimer = gpei.downInputTimer;
        lmbInputTimer = gpei.lmbInputTimer;
        rmbInputTimer = gpei.rmbInputTimer;
        mousePosTimer = gpei.mousePosTimer;
        
        GD.Print("GameRunState - leftInputTimer.Count = " + leftInputTimer.Count);
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
