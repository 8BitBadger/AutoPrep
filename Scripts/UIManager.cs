using Godot;
using System;
using EventCallback;

public class UIManager : Node
{
    Node2D menu;
    Node2D hud;
    Node2D wait;
    Node2D programming;
    Node2D run;
    Node2D win;
    Node2D lose;

    public override void _Ready()
    {
        menu = GetNode<Node2D>("Menu");
        hud = GetNode<Node2D>("HUD");
        wait = GetNode<Node2D>("Wait");
        programming = GetNode<Node2D>("Programming");
        run = GetNode<Node2D>("Run");
        win = GetNode<Node2D>("Win");
        lose = GetNode<Node2D>("Lose");
    }

    //Hides all the ui elements that are in the ui scen
    public void HideAllUI()
    {
        //Hide all the ui elements, used when transitioning from one scene to another
        menu.Hide();
        hud.Hide();
        wait.Hide();
        programming.Hide();
        run.Hide();
        win.Hide();
        lose.Hide();
    }
}
