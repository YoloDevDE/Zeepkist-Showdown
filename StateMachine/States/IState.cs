using Showdown3.StateMachine.Contexts;

namespace Showdown3.StateMachine.States;

public interface IState
{
    public IContext Context { get; }
    
    public void Enter();
    public void Exit();
}