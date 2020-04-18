using Godot;
using System;
using EventCallback;
public class MainCam : Camera2D
{
    Node target;

    private void SetTarget(CameraEvent cei)
    {
        target = cei.target;
    }
}
