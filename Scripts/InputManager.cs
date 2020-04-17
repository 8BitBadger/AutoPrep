using Godot;
using System;
using EventCallback;

public class InputManager : Node
{
    bool upCheck,downCheck,leftCheck, rightCheck;

    public override void _UnhandledInput(Godot.InputEvent @event)
    {
        if (@event is InputEventMouseButton || @event is InputEventKey)
        {
            InputCallbackEvent icei = new InputCallbackEvent();
            if (@event.IsActionPressed("LeftClick"))
            {
                icei.lmbClickPressed = true;
            }
            if (@event.IsActionReleased("LeftClick"))
            {
                icei.lmbClickRelease = true;
            }
            if (@event.IsActionPressed("RightClick"))
            {
                icei.rmbClickPressed = true;
            }
            if (@event.IsActionReleased("RightClick"))
            {
                icei.rmbClickRelease = true;
            }
            if (@event.IsActionPressed("MoveUp"))
            {
                icei.upPressed = true;
            }
            if (@event.IsActionReleased("MoveUp"))
            {
                icei.upRelease = true;
            }
            if (@event.IsActionPressed("MoveDown"))
            {
                icei.downPressed = true;
            }
            if (@event.IsActionReleased("MoveDown"))
            {
                icei.downRelease = true;
            }
            if (@event.IsActionPressed("MoveLeft"))
            {
                icei.leftPressed = true;
            }
            if (@event.IsActionReleased("MoveLeft"))
            {
                icei.leftRelease = true;
            }
            if (@event.IsActionPressed("MoveRight"))
            {
                icei.rightPressed = true;
            }
            if (@event.IsActionReleased("MoveRight"))
            {
                icei.rightRelease = true;
            }

            icei.FireEvent();
        }
    }
}
