using Godot;
using System;
using EventCallback;

public class Main : Node2D
{
    //The state manager for the games systems -------------------------------
    StateManager uiStateManager = new UIManager();
    //States for the games systems ------------------------------------------
    IState uiMenuState = new UIMenuState();
    IState uiHUDState = new UIHUDState();
    IState uiWinState = new UIWinState();
    IState uiLoseState = new UILoseState();
    IState uiWaitState = new UIWaitState();
    IState uiProgramingState = new UIProgramingState();
    IState uiRunState = new UIRunState();
    //The state manager for the game processes
    StateManager gameStateManager;
    //States for the games recording mechanic -------------------------------
    IState gameEmptyState = new GameEmptyState();
    IState gameWaitState = new GameWaitState();
    IState gameProgramState = new GameProgramState();
    IState gameRunState = new GameRunState();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Regestiring the events for the game states
        StartProgramEvent.RegisterListener(ProgramPressed);
        RunEvent.RegisterListener(RunPressed);
        //Init the ui state manager to the menu state
        uiStateManager.Init(uiMenuState);
        gameStateManager.Init(gameEmptyState);
    }
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        gameStateManager.Update();
        uiStateManager.Update();
    }

    private void ProgramPressed(StartProgramEvent rei)
    {
        gameStateManager.ChangeState(gameProgramState);
        uiStateManager.ChangeState(uiProgramingState);
    }

    private void StopProgrammingPressed(StopProgramEvent spei)
    {
        gameStateManager.ChangeState(gameWaitState);
        uiStateManager.ChangeState(uiWaitState);
    }

    private void RunPressed(RunEvent rei)
    {
        gameStateManager.ChangeState(gameRunState);
        uiStateManager.ChangeState(uiRunState);
    }

    public override void _ExitTree()

    {
        StartProgramEvent.UnregisterListener(ProgramPressed);
        RunEvent.UnregisterListener(RunPressed);
    }
}