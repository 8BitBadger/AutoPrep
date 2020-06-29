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
    PackedScene gunTurretScene = new PackedScene();
    PackedScene laserTurretScene = new PackedScene();
    //The node for the map that will be set to the instanced instance of the map packed scene
    Node gunTurret;
    Node laserTurret;
    //A list of enemies
    List<Node> turretList = new List<Node>();
    PackedScene goalScene = new PackedScene();
    Node goal;

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
        TileMap RealMap = GetNode<TileMap>("Map/ProgramMap");
        RealMap.QueueFree();
        displayMap = GetNode<TileMap>("Map/RealMap");
        displayMap.Visible = true;
        droidScene = ResourceLoader.Load("res://Scenes/Droid.tscn") as PackedScene;
        gunTurretScene = ResourceLoader.Load("res://Scenes/Enemies/GunTurret.tscn") as PackedScene;
        laserTurretScene = ResourceLoader.Load("res://Scenes/Enemies/LaserTurret.tscn") as PackedScene;
        goalScene = ResourceLoader.Load("res://Scenes/Goal.tscn") as PackedScene;


        //Instance player and gun towers
        BuildMap();
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
        if (upInputTimer.Count != 0 && upInputTimer.Keys.First() <= currentTime + 15 && upInputTimer.Keys.First() >= currentTime - 15)
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
            if (lmbInputTimer.Values.First() == InputActions.LEFT_CLICK_PRESSED) ((Gun)(droid.GetChild(2)).GetChild(0)).Fire();
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
    private void BuildMap()
    {
        for (int y = -21; y < 64; y++)
        {
            for (int x = 0; x < 73; x++)
            {

                //Spawn Payer and spawn gate
                if (displayMap.GetCell(x, y) == 6)
                {
                    //Load the player scene
                    displayMap.SetCell(x, y, 0);
                    droid = droidScene.Instance();
                    ((Node2D)droid).Position = new Vector2(x * 32 + 16, y * 32 + 16);
                    droid.Name = "Droid";
                    AddChild(droid);
                }
                //Used for instancing turrets
                if (displayMap.GetCell(x, y) == 4)
                {
                    RandomNumberGenerator rng = new RandomNumberGenerator();
                    rng.Randomize();
                    //Select a random turret to spawn
                    int spawnGunTurret = rng.RandiRange(0, 1);
                    if (spawnGunTurret == 1)
                    {
                        //Load the player scene
                        displayMap.SetCell(x, y, 8);
                        gunTurret = gunTurretScene.Instance();
                        ((Node2D)gunTurret).Position = new Vector2(x * 32 + 16, y * 32 + 16);
                        gunTurret.Name = "GunTurret";
                        AddChild(gunTurret);
                    }
                    else
                    {
                        //Load the player scene
                        displayMap.SetCell(x, y, 8);
                        laserTurret = laserTurretScene.Instance();
                        ((Node2D)laserTurret).Position = new Vector2(x * 32 + 16, y * 32 + 16);
                        laserTurret.Name = "LaserTurret";
                        AddChild(laserTurret);
                    }

                }
                //Spawn the goal for the map
                if (displayMap.GetCell(x, y) == 7)
                {
                    displayMap.SetCell(x, y, 0);
                    goal = goalScene.Instance();
                    ((Node2D)goal).Position = new Vector2(x * 32 + 16, y * 32 + 16);
                    goal.Name = "Goal";
                    AddChild(goal);
                }

            }
        }
    }

    //Run when the program is unloaded or closed
    public override void Exit()
    {
        displayMap.Visible = false;
    }
}
