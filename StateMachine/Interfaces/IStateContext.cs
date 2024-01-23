using ZeepSDK.Chat;

namespace Showdown3.StateMachine.Interfaces;

public interface IStateContext
{
    public IState State { get; set; }

    public void TransitionTo(IState nextState)
    {
        ChatApi.SendMessage($"Exiting: {State.GetType().Name}");
        State.Exit();
        ChatApi.SendMessage($"Entering: {nextState.GetType().Name}");
        nextState.Enter();
        State = nextState;
    }
}