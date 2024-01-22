using ZeepSDK.Chat;

namespace Showdown3.StateMachine.Interfaces;

public interface IStateMachine
{
    public IState CurrentState { get; set; }

    public void TransitionTo(IState nextState)
    {
        ChatApi.SendMessage($"Exiting: {CurrentState.GetType().Name}");
        CurrentState.Exit();
        ChatApi.SendMessage($"Entering: {nextState.GetType().Name}");
        nextState.Enter();
        CurrentState = nextState;
    }
}