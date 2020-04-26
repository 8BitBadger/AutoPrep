using Godot;
using System;
using EventCallback;

public class Main : Node2D
{
    //The state manager for the game processes
    StateManager gameStateManager = new GameStateManager();
    //States for the games recording mechanic -------------------------------
    State gameEmptyState = new GameEmptyState();
    State gameWaitState = new GameWaitState();
    State gameProgramState = new GameProgramState();
    State gameRunState = new GameRunState();

    //The scenes that need to be pre loaded for the game
    //The packed scene for the GameStateManagerScene that will be instanced later
    PackedScene GameStateManagerScene = new PackedScene();
    //The node for the GameStateManager that will be set to the instanced instance of the GameStateManagerScene packed scene
    Node gameStateManagerNode;
    //The packed scene for the player that will be instanced later
    PackedScene playerScene = new PackedScene();
    //The node for the player that will be set to the instanced instance of the players packed scene
    Node player;
    //The packed scene for the map that will be instanced later
    PackedScene mapScene = new PackedScene();
    //The node for the map that will be set to the instanced instance of the map packed scene
    Node map;
    //The packed scene for the map that will be instanced later
    //PackedScene enemyScene = new PackedScene();
    //The node for the map that will be set to the instanced instance of the map packed scene
    //Node enemy;
    //A list of enemies
    //List<Node> enemyList = new List<Node>();
    //The packed scene for the ui that will be instanced later
    PackedScene uiScene = new PackedScene();
    //The node for the ui that will be set to the instanced instance of the ui packed scene
    Node ui;
    //The packed scene for the camera that will be instanced later
    PackedScene camerScene = new PackedScene();
    //The node for the ui that will be set to the instanced instance of the ui packed scene
    Node camera;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //The events from the UI
        GetUIEvent.RegisterListener(GetUIInput);
        //Regestiring the events for the game states
        RunEvent.RegisterListener(RunPressed);

        Load();
        Init();
    }
    private void Load()
    {
        //Pre load the scenes for the game
        camerScene = ResourceLoader.Load("res://Scenes/Camera.tscn") as PackedScene;
        playerScene = ResourceLoader.Load("res://Scenes/Player.tscn") as PackedScene;
        mapScene = ResourceLoader.Load("res://Scenes/Map.tscn") as PackedScene;
        //enemyScene = ResourceLoader.Load("res://Scenes/Enemy.tscn") as PackedScene;
        uiScene = ResourceLoader.Load("res://Scenes/UI.tscn") as PackedScene;
    }

    private void Init()
    {
        //Init the ui state manager to the menu state
        gameStateManager.Init(gameEmptyState);
        //The UI of the game
        ui = uiScene.Instance();
        AddChild(ui);
        //At the begining of hte program set the ui state to the menu ui elemenet
        SendUIEvent suiei = new SendUIEvent();
        suiei.uiState = UIState.MENU;
        suiei.FireEvent();
    }
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        //Update call for the game state manager so that the states get updated
        gameStateManager.Update();
    }

    private void RunPressed(RunEvent rei)
    {
        gameStateManager.ChangeState(gameRunState);
    }

    private void GetUIInput(GetUIEvent guiei)
    {
        if (guiei.uiState == UIState.WAIT_HUD)
        {
            //Change the game state for the game mechanics loop
            gameStateManager.ChangeState(gameWaitState);
        }
        else if (guiei.uiState == UIState.PROGRAMMING_HUD)
        {
            //Change the game state for the game mechanics loop
            gameStateManager.ChangeState(gameProgramState);
        }
    }

    public override void _ExitTree()
    {
        RunEvent.UnregisterListener(RunPressed);
        GetUIEvent.UnregisterListener(GetUIInput);
    }
}