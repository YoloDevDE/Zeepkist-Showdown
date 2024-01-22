using Showdown3.StateMachine.Interfaces;
using ZeepkistClient;
using ZeepSDK.Racing;

namespace Showdown3.StateMachine;

public class StateRace : IState
{
    public StateRace(IStateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public IStateMachine StateMachine { get; }

    public void Enter()
    {
        new LobbyConfigurer()
            .SetTime(30)
            .Build();
        RacingApi.LevelLoaded += RacingApiOnRoundEnded;
    }

    private void RacingApiOnRoundEnded()
    {
        StateMachine.TransitionTo(new StateCheckResult(StateMachine));
    }

    public void Exit()
    {
        RacingApi.LevelLoaded -= RacingApiOnRoundEnded;
    }
}