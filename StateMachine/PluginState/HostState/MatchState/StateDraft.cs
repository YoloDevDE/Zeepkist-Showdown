using System;
using Showdown3.StateMachine.Interfaces;

namespace Showdown3.StateMachine.PluginState.HostState.MatchState;

public class StateDraft : IState
{
    private CountDown _countDown;

    public StateDraft(IStateContext stateContext)
    {
        StateContext = stateContext;
    }

    public IStateContext StateContext { get; }

    public void Enter()
    {
        _countDown = new CountDown(5);
        _countDown.Tick += CountDownOnTick;
        _countDown.CountdownEnded += CountDownOnCountdownEnded;
    }

    public void Exit()
    {
        _countDown.Tick -= CountDownOnTick;
        _countDown.CountdownEnded -= CountDownOnCountdownEnded;
        _countDown.End();
    }

    private void CountDownOnCountdownEnded()
    {
        StateContext.TransitionTo(new StateReadyCheck(StateContext));
    }

    private void CountDownOnTick(int seconds)
    {
        new ServerMessageBuilder()
            .SetColor(Color.red)
            .AddText($"Simulating Draft {seconds}")
            .BuildAndExecute();
    }
}