namespace Showdown3.StateMachine.Interfaces;

public interface IContext
{
    IState State { get; set; }
    public IContext TopContext { get; }

    public void TransitionTo(IState nextState)
    {
        State.Exit();
        nextState.Enter();
        State = nextState;
    }
}