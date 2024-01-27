using System;
using Showdown3.Entities.Match;
using Showdown3.Helper;
using Showdown3.StateMachine.Interfaces;
using ZeepSDK.Chat;
using ZeepSDK.Multiplayer;
using ZeepSDK.Playlist;
using ZeepSDK.Racing;

namespace Showdown3.StateMachine.PluginState.HostState.MatchState;

public class StatePreRace : IState
{
    private Countdown _countdown;
    private Match _match;

    public StatePreRace(IContext context)
    {
        Context = context;
        _match = ((MatchContext)context).Match;
    }


    public void Enter()
    {
        _countdown = new Countdown(10);
        _countdown.OnTick += CountdownOnTick;
        _countdown.OnCountdownEnd += CountdownOnCountdownEnded;
        RacingApi.LevelLoaded += RacingApiOnLevelLoaded;
        MultiplayerApi.SetNextLevelIndex(_match.RoundIndex);
        _countdown.Start();
    }

    public void Exit()
    {
        _countdown.OnTick -= CountdownOnTick;
        _countdown.OnCountdownEnd -= CountdownOnCountdownEnded;
        RacingApi.LevelLoaded -= RacingApiOnLevelLoaded;
        _countdown.Stop();
        ((MatchContext)Context).Match = _match;
    }

    public IContext Context { get; }

    private void RacingApiOnLevelLoaded()
    {
        Context.TransitionTo(new StateRace(Context));
    }

    private void CountdownOnCountdownEnded()
    {
        ChatApi.SendMessage("/fs");
    }

    private void CountdownOnTick(int seconds)
    {
        new ServerMessageBuilder()
            .SetColor(Color.red)
            .AddText($"{_match.TeamA} vs {_match.TeamB}")
            .AddBreak()
            .AddText($"Starting in: {_countdown.GetFormattedRemainingTime()}")
            .BuildAndExecute();
    }
}