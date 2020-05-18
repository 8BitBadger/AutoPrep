using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using EventCallback;

public class GameRunState : State
{
    Dictionary<ulong, InputActions> leftInputTimer = new Dictionary<ulong, InputActions>();
    Dictionary<ulong, InputActions> rightInputTimer = new Dictionary<ulong, InputActions>();
    Dictionary<ulong, InputActions> upInputTimer = new Dictionary<ulong, InputActions>();
    Dictionary<ulong, InputActions> downInputTimer = new Dictionary<ulong, InputActions>();
    Dictionary<ulong, InputActions> lmbInputTimer = new Dictionary<ulong, InputActions>();
    Dictionary<ulong, InputActions> rmbInputTimer = new Dictionary<ulong, InputActions>();
    Dictionary<ulong, Vector2> mousePosTimer = new Dictionary<ulong, Vector2>();

    //The packed scene for the map that will be instanced later
    PackedScene mapScene = new PackedScene();
    //The node for the map that will be set to the instanced instance of the map packed scene
    Node map;
    //The tilemap to display
    TileMap displayMap;
    PackedScene droidScene = new PackedScene();
    //The node for the player that will be set to the instanced instance of the players packed scene
    Node droid;
    //The packed scene for the map that will be instanced later
    //PackedScene enemyScene = new PackedScene();
    //The node for the map that will be set to the instanced instance of the map packed scene
    //Node enemy;
    //A list of enemies
    //List<Node> enemyList = new List<Node>();

    //When the timer was started
    ulong timerStarted;
    //Run when the state starts up
    public override void Init()
    {
        //We use the event manager to grab the values, we do not call the fire event
        //becuase we dont want to store any new values just get the current ones
        GetProgramEvent gpei = new GetProgramEvent();
        gpei.FireEvent();
        leftInputTimer = gpei.leftInputTimer;
        rightInputTimer = gpei.rightInputTimer;
        upInputTimer = gpei.upInputTimer;
        downInputTimer = gpei.downInputTimer;
        lmbInputTimer = gpei.lmbInputTimer;
        rmbInputTimer = gpei.rmbInputTimer;
        mousePosTimer = gpei.mousePosTimer;

        //Load the map resource scene and instance it as a child of the GameProgramState node
        mapScene = ResourceLoader.Load("res://Scenes/Map.tscn") as PackedScene;
        map = mapScene.Instance();
        AddChild(map);
        displayMap = GetNode<TileMap>("Map/RealMap");
        displayMap.Visible = true;

        //Load the player scene
        droidScene = ResourceLoader.Load("res://Scenes/Droid.tscn") as PackedScene;
        droid = droidScene.Instance();
        ((Node2D)droid).Position = Vector2.One * 64;
        AddChild(droid);

        //Set up the camera follow for hte player
        CameraEvent cei = new CameraEvent();
        cei.target = (Node2D)droid;
        cei.FireEvent();

        //enemyScene = ResourceLoader.Load("res://Scenes/Enemy.tscn") as PackedScene;

        //Grab the time the state started after all the initializations have completely run
        timerStarted = OS.GetTicksMsec();
    }
    //Run in the games loop
    public override void Update()
    {
        ulong currentTime = OS.GetTicksMsec() - timerStarted;

        if (leftInputTimer.Count != 0 && leftInputTimer.Keys.First() <= currentTime + 15 && leftInputTimer.Keys.First() >= currentTime - 15)
        {
            //Get the value from the dictionaries first entry
            //Note the first entry in the dictionary might nit be the first entry added as c# dictionaries don't work that way
            //double check if it works correctly
            if (leftInputTimer.Values.First() == InputActions.LEFT_PRESSED) ((SimulateMovement)droid).left = true;
            else if (leftInputTimer.Values.First() == InputActions.LEFT_RELEASED) ((SimulateMovement)droid).left = false;
            //Remove the first entry in the dictionary
            leftInputTimer.Remove(leftInputTimer.Keys.First());

        }
        if (rightInputTimer.Count != 0 && rightInputTimer.Keys.First() <= currentTime + 15 && rightInputTimer.Keys.First() >= currentTime - 15)
        {
            //Get the value from the dictionaries first entry
            if (rightInputTimer.Values.First() == InputActions.RIGHT_PRESSED) ((SimulateMovement)droid).right = true;
            else if (rightInputTimer.Values.First() == InputActions.RIGHT_RELEASED) ((SimulateMovement)droid).right = false;
            //Remove the first entry in the dictionary
            rightInputTimer.Remove(rightInputTimer.Keys.First());
        }
        if (upInputTimer.Count != 0 && upInputTimer.Keys.First() <= currentTime + 15 && upInputTimer.Keys.First() >=currentTime - 15)
        {
            //Get the value from the dictionaries first entry
            if (upInputTimer.Values.First() == InputActions.UP_PRESSED) ((SimulateMovement)droid).up = true;
            else if (upInputTimer.Values.First() == InputActions.UP_RELEASED) ((SimulateMovement)droid).up = false;
            //Remove the first entry in the dictionary
            upInputTimer.Remove(upInputTimer.Keys.First());
        }
        if (downInputTimer.Count != 0 && downInputTimer.Keys.First() <= currentTime + 15 && downInputTimer.Keys.First() >= currentTime - 15)
        {
            //Get the value from the dictionaries first entry
            if (downInputTimer.Values.First() == InputActions.DOWN_PRESSED) ((SimulateMovement)droid).down = true;
            else if (downInputTimer.Values.First() == InputActions.DOWN_RELEASED) ((SimulateMovement)droid).down = false;
            //Remove the first entry in the dictionary
            downInputTimer.Remove(downInputTimer.Keys.First());
        }
        
        if (lmbInputTimer.Count != 0 && lmbInputTimer.Keys.First() <= currentTime + 15 && lmbInputTimer.Keys.First() >= currentTime - 15)
        {
            //I dont like usign the GetChild method, if the position of the child node weapon is changed the code will break with no real error
            if(lmbInputTimer.Values.First() == InputActions.LEFT_CLICK_PRESSED) ((Gun)droid.GetChild(2)).Fire();
            //Remove the first entry in the dictionary
            lmbInputTimer.Remove(lmbInputTimer.Keys.First());
        }
        /*
        if (rmbInputTimer.Keys.First() >= currentTime)
        {

        }
        */
        if (mousePosTimer.Count != 0 && mousePosTimer.Keys.First() <= currentTime + 15 && mousePosTimer.Keys.First() >= currentTime - 15)
        {
            //Get the value from the dictionaries first entry
            ((SimulateMovement)droid).mousePos = mousePosTimer.Values.First();

            //Remove the first entry in the dictionary
            mousePosTimer.Remove(mousePosTimer.Keys.First());
        }


        //1. Find the key in the dictionaries
        //2. Input the dictionary value to the droid movement controller script
        //3. Remove the dictionary entry

    }
    //Run when the program is unloaded or closed
    public override void Exit()
    {
        displayMap.Visible = false;
    }
}
