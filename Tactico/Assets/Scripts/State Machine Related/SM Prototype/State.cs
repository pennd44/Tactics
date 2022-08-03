public abstract class State
{
    // protected StateMachine stateMachine;
    // public State(StateMachine stateMachine)
    // {
    //     this.stateMachine = stateMachine;
    // }
    public virtual void enter() { }
    public virtual void handleInput() { }
    public abstract void Tick();
    public virtual void exit() { }
}
