using Godot;
using System;
using EventCallback;

public class StartPressed : Button
{
    // Called when the node enters the scene tree for the first time.
    public override void _Pressed()
    {
        SendUIEvent suiei = new SendUIEvent();
        suiei.uiState = UIState.WAIT_HUD;
        suiei.FireEvent();
    }
}
