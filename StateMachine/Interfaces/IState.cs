using ZeepSDK.Chat;

namespace Showdown3.StateMachine.Interfaces;

public interface IState
{
    public IStateMachine StateMachine { get; }

    public void Enter();
    
    public void Exit();


}