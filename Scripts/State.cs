using System;
using Godot;

//public abstract class State
public interface IState
{
    //Gets called when the state starts up for any things that need to be pre loaded
    //public abstract void Init();
    void Init();
    //Updates any calculations tha need to be done by the state
    //public abstract void Update();
    void Update();
    //Called when the state exits to unload any memory that is not needed anymore
    //public abstract void Exit();
    void Exit();
}