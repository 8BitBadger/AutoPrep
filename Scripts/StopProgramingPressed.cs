using Godot;
using System;
using EventCallback;

public class StopProgramingPressed : Button
{
    public override void _Pressed()
    {
        GetUIEvent guiei = new GetUIEvent();
        guiei.uiState = UIState.WAIT_HUD;
        guiei.FireEvent();
    }
}
