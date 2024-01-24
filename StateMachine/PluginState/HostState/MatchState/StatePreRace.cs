using System;
using Showdown3.StateMachine.Interfaces;
using ZeepSDK.Chat;
using ZeepSDK.Racing;

namespace Showdown3.StateMachine.PluginState.HostState.MatchState;

public class StatePreRace : IState
{
    private CountDown _countDown;

    public StatePreRace(IContext context)
    {
        Context = context;
    }

    public IContext Context { get; }

    public void Enter()
    {
        _countDown = new CountDown(10);
        _countDown.Tick += CountDownOnTick;
        _countDown.CountdownEnded += CountDownOnCountdownEnded;
        RacingApi.LevelLoaded += RacingApiOnLevelLoaded;
    }

    public void Exit()
    {
        _countDown.Tick -= CountDownOnTick;
        _countDown.CountdownEnded -= CountDownOnCountdownEnded;
        RacingApi.LevelLoaded -= RacingApiOnLevelLoaded;
    }

    private void RacingApiOnLevelLoaded()
    {
        Context.TransitionTo(new StateRace(Context));
    }

    private void CountDownOnCountdownEnded()
    {
        ChatApi.SendMessage("/fs");
    }

    private void CountDownOnTick(int seconds)
    {
        new ServerMessageBuilder()
            .SetColor(Color.red)
            .AddText("X vs Y")
            .AddBreak()
            .AddText($"Starting in: {TimeSpan.FromSeconds(seconds).Duration():ss}")
            .BuildAndExecute();
    }
}