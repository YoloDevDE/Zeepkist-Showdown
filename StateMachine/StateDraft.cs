using System;
using Showdown3.StateMachine.Interfaces;

namespace Showdown3.StateMachine;

public class StateDraft : IState
{
    CountDown _countDown;

    public StateDraft(IStateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public IStateMachine StateMachine { get; }

    public void Enter()
    {
        new ServerMessageBuilder()
            .SetColor(Color.red)
            .AddText("Drafting started")
            .BuildAndExecute();
        _countDown = new CountDown(10);
        _countDown.Tick += CountDownOnTick;
        _countDown.CountdownEnded += CountDownOnCountdownEnded;
    }

    private void CountDownOnCountdownEnded(object sender, EventArgs e)
    {
        StateMachine.TransitionTo(new StateReadyCheck(StateMachine));
    }

    private void CountDownOnTick(object sender, int e)
    {
        new ServerMessageBuilder()
            .SetColor(Color.red)
            .AddText($"Drafting started {e}")
            .BuildAndExecute();
    }

    public void Exit()
    {
    }
}