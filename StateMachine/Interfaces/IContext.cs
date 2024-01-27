namespace Showdown3.StateMachine.Interfaces;

public interface IContext
{
    public IState State { get; set; }

    public IContext TopContext { get; }

    public void TransitionTo(IState nextState)
    {
        State?.Exit();
        State = nextState;
        State.Enter();
    }
}