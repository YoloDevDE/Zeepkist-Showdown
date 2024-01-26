namespace Showdown3.StateMachine.Interfaces;

public interface IState
{
    public IContext Context { get; }
    public void Enter();

    public void Exit();
}