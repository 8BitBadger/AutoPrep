using Godot;
using System;
using EventCallback;

public class StopProgramingPressed : Button
{
    public override void _Pressed()
    {
        SendUIEvent suiei = new SendUIEvent();
        suiei.uiState = UIState.WAIT_HUD;
        suiei.FireEvent();
    }
}
