using Godot;
using System;
using EventCallback;

public class InputMove : KinematicBody2D
{
    //The speed of the player
    int speed = 350;
    //The acceleration of the player
    //int accel = 100;
    //The deccelaration of the player
    //int deccel = 100;
     float turnSpeed = 0.07f;

    //The movement keys are pressed or not
    bool up, down, left, right;
    //the velocity of the charecter
    Vector2 inputVelocity = new Vector2();
    Vector2 velocity = new Vector2();
    //The global position for hte mouse
    Vector2 globalMousePos;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        InputCallbackEvent.RegisterListener(GetInput);
        MouseInputCallbackEvent.RegisterListener(GetMousePos);
    }
    private void GetInput(InputCallbackEvent icei)
    {
        if (icei.upPressed) up = true;
        else if (icei.upRelease) up = false;

        if (icei.downPressed) down = true;
        else if (icei.downRelease) down = false;

        if (icei.leftPressed) left = true;
        else if (icei.leftRelease) left = false;

        if (icei.rightPressed) right = true;
        else if (icei.rightRelease) right = false;
    }

    private void GetMousePos(MouseInputCallbackEvent micei)
    {
        globalMousePos = micei.mousePos;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        if (up) inputVelocity.y = -1;
        else if (down) inputVelocity.y = 1;
        else inputVelocity.y = 0;
        if (left) inputVelocity.x = -1;
        else if (right) inputVelocity.x = 1;
        else inputVelocity.x = 0;

        /*
                inputVelocity = inputVelocity.Normalized();

                if (inputVelocity != Vector2.Zero)
                {
                    velocity += inputVelocity * accel;

                    velocity.x = Mathf.Clamp(velocity.x, -speed, speed);
                    velocity.y = Mathf.Clamp(velocity.y, -speed, speed);
                }
                else
                {
                    if(velocity.Length() > deccel * delta)
                    {
                        velocity -= velocity.Normalized() * deccel * delta;
                    }
                    else
                    {
                        velocity = Vector2.Zero;
                    }  
                }
        */
        //inputVelocity = inputVelocity.Normalized();
        //velocity.x = Mathf.Lerp(velocity.x, velocity.x * inputVelocity.x * speed, speed * delta);
        //velocity.y = Mathf.Lerp(velocity.y, velocity.y * inputVelocity.y * speed, speed * delta);


      
        //Look in the directio of the mouses global position
        float angleToMouse = GetAngleTo(globalMousePos);
        if(Mathf.Abs(angleToMouse) < turnSpeed)
        {
            Rotation += angleToMouse;
        }
        else
        {
            if(angleToMouse > 0) Rotation += turnSpeed;
            if(angleToMouse < 0) Rotation -= turnSpeed;
        }

        velocity = inputVelocity.Normalized() * speed;
        //Move and slide the rigidbaody 2d
        MoveAndSlide(velocity);
    }
    public override void _ExitTree()
    {
        InputCallbackEvent.UnregisterListener(GetInput);
        MouseInputCallbackEvent.UnregisterListener(GetMousePos);
    }
}
