using Showdown3.Entities;
using Showdown3.Entities.Match;
using Showdown3.Helper;
using Showdown3.StateMachine.Interfaces;
using ZeepkistClient;
using ZeepSDK.Leaderboard;
using ZeepSDK.Multiplayer;
using ZeepSDK.Racing;

namespace Showdown3.StateMachine.PluginState.HostState.MatchState;

public class StateRace : IState
{
    private Match _match;
    private Round _round;

    public StateRace(IContext context)
    {
        Context = context;
        _match = ((MatchContext)Context).Match;
        _round = _match.Rounds[_match.RoundIndex];
    }


    public void Enter()
    {
        new LobbyConfigurer()
            .SetTime(300)
            .Build();
        RacingApi.LevelLoaded += RacingApiOnRoundEnded;
        ZeepkistNetwork.PlayerResultsChanged += PlayerResultsChanged;
        RacingApi.RoundEnded += EndRace;
    }

    private void EndRace()
    {
        Context.TransitionTo(new StateCheckResult(Context));
    }

    private void PlayerResultsChanged(ZeepkistNetworkPlayer player)
    {
        var leaderboard = ZeepkistNetwork.Leaderboard;
        var i = 1;
        foreach (var item in leaderboard)
        {
            foreach (var racer in _match.GetAllRacersInCurrentMatch())
            {
                if (racer.SteamId == item.SteamID)
                {
                    _round.UpdateRacerScore(racer, i);
                    break;
                }
            }

            i++;
        }
    }

    public void Exit()
    {
        RacingApi.LevelLoaded -= RacingApiOnRoundEnded;
        ZeepkistNetwork.PlayerResultsChanged -= PlayerResultsChanged;
        RacingApi.RoundEnded -= EndRace;
        _match.Rounds[_match.RoundIndex] = _round;
        ((MatchContext)Context).Match = _match;
    }

    public IContext Context { get; }

    private void RacingApiOnRoundEnded()
    {
        Context.TransitionTo(new StateCheckResult(Context));
    }
}