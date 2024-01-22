using System;
using Showdown3.StateMachine.Interfaces;
using ZeepSDK.Chat;
using ZeepSDK.Multiplayer;
using ZeepSDK.Racing;

namespace Showdown3.StateMachine;

public class StateRaceStarting : IState
{
    CountDown _countDown;

    public StateRaceStarting(IStateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public IStateMachine StateMachine { get; }

    public void Enter()
    {
        new ServerMessageBuilder()
            .SetColor(Color.red)
            .AddText("RaceStarting started")
            .BuildAndExecute();
        _countDown = new CountDown(10);
        _countDown.Tick += CountDownOnTick;
        _countDown.CountdownEnded += CountDownOnCountdownEnded;
        RacingApi.LevelLoaded += RacingApiOnLevelLoaded;
    }

    private void RacingApiOnLevelLoaded()
    {
        StateMachine.TransitionTo(new StateRace(StateMachine));
    }

    private void CountDownOnCountdownEnded(object sender, EventArgs e)
    {
        ChatApi.SendMessage("/fs");
    }

    private void CountDownOnTick(object sender, int e)
    {
        new ServerMessageBuilder()
            .SetColor(Color.red)
            .AddText($"RaceStarting started {e}")
            .BuildAndExecute();
    }

    public void Exit()
    {
        _countDown.Tick -= CountDownOnTick;
        _countDown.CountdownEnded -= CountDownOnCountdownEnded;
        RacingApi.LevelLoaded -= RacingApiOnLevelLoaded;
    }
}