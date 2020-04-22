using Godot;
using System;
using EventCallback;

public class ProgramPressed : Button
{
    public override void _Pressed()
    {
        SendUIEvent suiei = new SendUIEvent();
        suiei.uiState = UIState.PROGRAMMING_HUD;
        suiei.FireEvent();
    }
}
