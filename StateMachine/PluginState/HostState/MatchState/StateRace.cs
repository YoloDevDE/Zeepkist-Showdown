using Showdown3.StateMachine.Interfaces;
using ZeepSDK.Racing;

namespace Showdown3.StateMachine.PluginState.HostState.MatchState;

public class StateRace : IState
{
    public StateRace(IContext context)
    {
        Context = context;
    }


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

    public IContext Context { get; }

    private void RacingApiOnRoundEnded()
    {
        Context.TransitionTo(new StateCheckResult(Context));
    }
}