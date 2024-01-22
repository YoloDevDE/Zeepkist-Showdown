using System;
using Showdown3.StateMachine.Interfaces;

namespace Showdown3.StateMachine;

public class StateReadyCheck : IState
{
    CountDown _countDown;

    public StateReadyCheck(IStateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public IStateMachine StateMachine { get; }

    public void Enter()
    {
        new ServerMessageBuilder()
            .SetColor(Color.red)
            .AddText("ReadyCheck started")
            .BuildAndExecute();
        new LobbyConfigurer()
            .SetTime(84600)
            .Build();
        _countDown = new CountDown(10);
        _countDown.Tick += CountDownOnTick;
        _countDown.CountdownEnded += CountDownOnCountdownEnded;
    }

    private void CountDownOnCountdownEnded(object sender, EventArgs e)
    {
        StateMachine.TransitionTo(new StateRaceStarting(StateMachine));
    }

    private void CountDownOnTick(object sender, int e)
    {
        new ServerMessageBuilder()
            .SetColor(Color.red)
            .AddText($"ReadyCheck started {e}")
            .BuildAndExecute();
    }

    public void Exit()
    {
        _countDown.Tick -= CountDownOnTick;
        _countDown.CountdownEnded -= CountDownOnCountdownEnded;
    }
}