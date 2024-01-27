using System.Linq;
using Showdown3.Entities.Match;
using Showdown3.Helper;
using Showdown3.StateMachine.Interfaces;
using ZeepkistClient;
using ZeepSDK.Multiplayer;

namespace Showdown3.StateMachine.PluginState.HostState.MatchState;

public class State2WaitingForRacers : IState
{
    private Match _match;

    public State2WaitingForRacers(IContext context)
    {
        Context = context;
    }

    public void Enter()
    {
        _match = ((MatchContext)Context).Match;
        MultiplayerApi.PlayerJoined += Update;
        MultiplayerApi.PlayerLeft += Update;
        Update();
    }

    public void Exit()
    {
        MultiplayerApi.PlayerJoined -= Update;
        MultiplayerApi.PlayerLeft -= Update;
    }

    public IContext Context { get; }

    private void Update()
    {
        var racersInGame = _match.GetAllRacersInCurrentMatch()
            .Where(racer => ZeepkistNetwork.Players.Any(p => p.Value.SteamID == racer.SteamId))
            .ToList();

        var racersNotInGame = _match.GetAllRacersInCurrentMatch()
            .Except(racersInGame)
            .Select(r => r.Name)
            .ToList();
        racersNotInGame.Clear();
        if (!racersNotInGame.Any())
        {
            Context.TransitionTo(new StateWaitingForHost(Context));
            return;
        }

        new ServerMessageBuilder()
            .SetColor(Color.white)
            .AddText("Waiting for:")
            .AddBreak()
            .AddText(string.Join(", ", racersNotInGame));
    }


    private void Update(ZeepkistNetworkPlayer player)
    {
        Update();
    }
}