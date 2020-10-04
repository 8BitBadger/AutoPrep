using Godot;
using System;
using EventCallback;
public class MainCam : Camera2D
{
    Node2D target = null;

    public override void _Ready()
    {
        CameraEvent.RegisterListener(SetTarget);
    }

    private void SetTarget(CameraEvent cei)
    {
        target = cei.target;
        //If the target is set, set it as a child of that node to 
        if (target != null)
        {
            GetParent().RemoveChild(this);
            target.AddChild(this);
        }
        if (cei.smoothing) SmoothingEnabled = true;

        SmoothingSpeed = cei.smoothingSpeed;

        DragMarginVEnabled = cei.dragMarginVertical;
        DragMarginHEnabled = cei.dragMarginHorizontal;

        DragMarginTop = cei.dragMarginTop;
        DragMarginBottom = cei.dragMarginBottom;
        DragMarginLeft = cei.drangMarginLeft;
        DragMarginRight = cei.dragMarginRight;
    }
}
