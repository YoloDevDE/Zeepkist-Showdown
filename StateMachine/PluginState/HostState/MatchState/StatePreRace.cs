using System;
using Showdown3.Helper;
using Showdown3.StateMachine.Interfaces;
using ZeepSDK.Chat;
using ZeepSDK.Racing;

namespace Showdown3.StateMachine.PluginState.HostState.MatchState;

public class StatePreRace : IState
{
    private Countdown _countdown;

    public StatePreRace(IContext context)
    {
        Context = context;
    }


    public void Enter()
    {
        _countdown = new Countdown(10);
        _countdown.OnTick += CountdownOnTick;
        _countdown.OnCountdownEnd += CountdownOnCountdownEnded;
        RacingApi.LevelLoaded += RacingApiOnLevelLoaded;
    }

    public void Exit()
    {
        _countdown.OnTick -= CountdownOnTick;
        _countdown.OnCountdownEnd -= CountdownOnCountdownEnded;
        _countdown.Stop();
        RacingApi.LevelLoaded -= RacingApiOnLevelLoaded;
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
            .AddText("X vs Y")
            .AddBreak()
            .AddText($"Starting in: {TimeSpan.FromSeconds(seconds).Duration():ss}")
            .BuildAndExecute();
    }
}