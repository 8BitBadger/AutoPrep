using System;
using Godot;

//public abstract class State
//public interface IState
public abstract class State : Node
{
    //Gets called when the state starts up for any things that need to be pre loaded
    //public abstract void Init();
    public abstract void Init();
    //Updates any calculations tha need to be done by the state
    //public void Update();
    public abstract void Update();
    //Called when the state exits to unload any memory that is not needed anymore
    //public void Exit();
    public abstract void Exit();
}