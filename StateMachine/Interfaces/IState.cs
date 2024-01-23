namespace Showdown3.StateMachine.Interfaces;

public interface IState
{
    public IStateContext StateContext { get; }
    
    

    public void Enter();

    public void Exit();
}