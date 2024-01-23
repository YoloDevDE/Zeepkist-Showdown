using Showdown3.Commands;
using Showdown3.StateMachine.Interfaces;

namespace Showdown3.StateMachine.PluginState;

public class StateOff : IState
{
    public StateOff(IStateContext stateContext)
    {
        StateContext = stateContext;
    }


    public IStateContext StateContext { get; }

    public void Enter()
    {
        CommandStart.OnHandle += OnCommandStart;
        CommandStop.OnHandle += OnCommandStop;
    }


    public void Exit()
    {
        CommandStart.OnHandle -= OnCommandStart;
        CommandStop.OnHandle -= OnCommandStop;
    }

    private void OnCommandStop()
    {
        TaggedMessenger.Value.LogWarning("already stopped");
    }

    private void OnCommandStart()
    {
        StateContext.TransitionTo(new StateOn(StateContext));
    }
}