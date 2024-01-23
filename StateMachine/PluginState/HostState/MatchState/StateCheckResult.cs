using Showdown3.StateMachine.Interfaces;

namespace Showdown3.StateMachine.PluginState.HostState.MatchState;

public class StateCheckResult : IState
{
    public StateCheckResult(IStateContext stateContext)
    {
        StateContext = stateContext;
    }

    public IStateContext StateContext { get; }

    public void Enter()
    {
        if (((StateHost)StateContext).Match.CheckIfWinContitionIsMet())
            StateContext.TransitionTo(new StateWaiting(StateContext));
        else
            StateContext.TransitionTo(new StateReadyCheck(StateContext));
    }

    public void Exit()
    {
    }
}