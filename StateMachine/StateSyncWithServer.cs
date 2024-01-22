using Showdown3.StateMachine.Interfaces;

namespace Showdown3.StateMachine;

public class StateSyncWithServer : IState
{
    public StateSyncWithServer(IStateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public IStateMachine StateMachine { get; }

    public void Enter()
    {
        StateMachine.TransitionTo(ShowdownRepository.GetStateFromServer(StateMachine));
    }

    public void Exit()
    {
    }
}

public class ShowdownRepository
{
    public static IState GetStateFromServer(IStateMachine stateMachine)
    {
        return new StateWaitForCommand(stateMachine);
    }
}

