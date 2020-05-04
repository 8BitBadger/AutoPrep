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

        //Load the player scene
        droidScene = ResourceLoader.Load("res://Scenes/Player.tscn") as PackedScene;
        droid = droidScene.Instance();
        AddChild(droid);

        //enemyScene = ResourceLoader.Load("res://Scenes/Enemy.tscn") as PackedScene;

        GD.Print("GameRunState - leftInputTimer.Count = " + leftInputTimer.Count);
        GD.Print("GameRunState - mousePosTimer.Count = " + mousePosTimer.Count);
        //Grab the time the state started after all the initializations have completely run
        timerStarted = OS.GetTicksMsec();
    }
    //Run in the games loop
    public override void Update()
    {
        ulong currentTime = OS.GetTicksMsec() - timerStarted;

        if(leftInputTimer.Keys.First() == currentTime)
        {

        }

        //1. Find the key in the dictionaries
        //2. Input the dictionary value to the droid movement controller script
        //3. Remove the dictionary entry

    }
    //Run when the program is unloaded or closed
    public override void Exit()
    {
    }
}
