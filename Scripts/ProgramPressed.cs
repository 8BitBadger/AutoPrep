using Godot;
using System;
using EventCallback;

public class ProgramPressed : Button
{
    public override void _Pressed()
    {
        GetUIEvent guiei = new GetUIEvent();
        guiei.uiState = UIState.PROGRAMMING_HUD;
        guiei.FireEvent();
    }
}
