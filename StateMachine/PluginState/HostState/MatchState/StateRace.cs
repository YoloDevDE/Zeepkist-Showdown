using Showdown3.StateMachine.Interfaces;
using ZeepSDK.Racing;

namespace Showdown3.StateMachine.PluginState.HostState.MatchState;

public class StateRace : IState
{
    public StateRace(IStateContext stateContext)
    {
        StateContext = stateContext;
    }

    public IStateContext StateContext { get; }

    public void Enter()
    {
        new LobbyConfigurer()
            .SetTime(30)
            .Build();
        RacingApi.LevelLoaded += RacingApiOnRoundEnded;
    }

    public void Exit()
    {
        RacingApi.LevelLoaded -= RacingApiOnRoundEnded;
    }

    private void RacingApiOnRoundEnded()
    {
        StateContext.TransitionTo(new StateCheckResult(StateContext));
    }
}