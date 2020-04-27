using Godot;
using System;
using System.Collections.Generic;
using EventCallback;

public class GameStateManager : StateManager
{
    Dictionary<ulong, InputActions> leftInputTimer = new Dictionary<ulong, InputActions>();
    Dictionary<ulong, InputActions> rightInputTimer = new Dictionary<ulong, InputActions>();
    Dictionary<ulong, InputActions> upInputTimer = new Dictionary<ulong, InputActions>();
    Dictionary<ulong, InputActions> downInputTimer = new Dictionary<ulong, InputActions>();
    Dictionary<ulong, InputActions> lmbInputTimer = new Dictionary<ulong, InputActions>();
    Dictionary<ulong, InputActions> rmbInputTimer = new Dictionary<ulong, InputActions>();
    Dictionary<ulong, Vector2> mousePosTimer = new Dictionary<ulong, Vector2>();

    //States for the games recording mechanic -------------------------------
    public override void Init(Node state)
    {
        base.Init(state);
        SendProgramEvent.RegisterListener(StoreProgram);
        GetProgramEvent.RegisterListener(GetProgram);
    }
    private void StoreProgram(SendProgramEvent spei)
    {
        leftInputTimer = spei.leftInputTimer;
        rightInputTimer = spei.rightInputTimer;
        upInputTimer = spei.upInputTimer;
        downInputTimer = spei.downInputTimer;
        lmbInputTimer = spei.lmbInputTimer;
        rmbInputTimer = spei.rmbInputTimer;
        mousePosTimer = spei.mousePosTimer;
    }
    private void GetProgram(GetProgramEvent gpei)
    {
        gpei.leftInputTimer = leftInputTimer;
        gpei.rightInputTimer = rightInputTimer;
        gpei.upInputTimer = upInputTimer;
        gpei.downInputTimer = downInputTimer;
        gpei.lmbInputTimer = lmbInputTimer;
        gpei.rmbInputTimer = rmbInputTimer;
        gpei.mousePosTimer = mousePosTimer;
    }
    public override void Exit()
    {
        base.Exit();
        SendProgramEvent.UnregisterListener(StoreProgram);
        GetProgramEvent.UnregisterListener(GetProgram);
    }
}
