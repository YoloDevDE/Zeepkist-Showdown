using Showdown3.Commands;
using Showdown3.Entities;
using Showdown3.Entities.Match;
using Showdown3.Helper;
using Showdown3.StateMachine.Interfaces;
using ZeepkistClient;
using ZeepSDK.Chat;
using ZeepSDK.Playlist;

namespace Showdown3.StateMachine.PluginState.HostState.MatchState;

public class State1SetupMatch : IState
{
    private Match _match;

    public State1SetupMatch(IContext context)
    {
        Context = context;
    }

    public IContext Context { get; }

    public void Enter()
    {
        CommandMatchSet.OnHandle += SetMatch;
        CommandMatchCreate.OnHandle += CreateMatch;
        LobbyConfigurer.HoFLobby();
    }

    public void Exit()
    {
        CommandMatchSet.OnHandle -= SetMatch;
        CommandMatchCreate.OnHandle -= CreateMatch;
    }

    private void CreateMatch()
    {
        if (_match == null) TaggedMessenger.Value.LogError("No Match selected");
        else Context.TransitionTo(new State2WaitingForRacers(Context));
    }

    private async void SetMatch(string identifier)
    {
        _match = await Match.GetMatchFromServerAsync(identifier.ToUpper());
        _match.AvailableMaps = await Level.GetMatchFromServerAsync();
        ChatApi.SendMessage($"{string.Join(", ", _match.AvailableMaps)}");
        ((MatchContext)Context).Match = _match;

        ChatApi.SendMessage($"<br>Match set to '{identifier}':<br>{_match.TeamA.Tag} vs. {_match.TeamB.Tag}");
    }
}