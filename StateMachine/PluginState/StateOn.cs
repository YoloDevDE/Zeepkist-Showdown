using Showdown3.Commands;
using Showdown3.StateMachine.Interfaces;
using Showdown3.StateMachine.PluginState.HostState;

namespace Showdown3.StateMachine.PluginState;

public class StateOn : IStateContextConnector
{
    public StateOn(IStateContext stateContext)
    {
        StateContext = stateContext;
        State = new HostContext(this);
    }

    public IStateContext StateContext { get; }
    public IState State { get; set; }

    public void Enter()
    {
        CommandStart.OnHandle += OnCommandStart;
        CommandStop.OnHandle += OnCommandStop;
        TaggedMessenger.Value.LogSuccess("started");

        State.Enter();
    }


    public void Exit()
    {
        CommandStart.OnHandle -= OnCommandStart;
        CommandStop.OnHandle -= OnCommandStop;
        TaggedMessenger.Value.LogSuccess("stopped");

        State.Exit();
    }

    private void OnCommandStop()
    {
        StateContext.TransitionTo(new StateOff(StateContext));
    }

    private void OnCommandStart()
    {
        TaggedMessenger.Value.LogWarning("already started");
    }
}