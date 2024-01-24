using Showdown3.Commands;
using Showdown3.StateMachine.Interfaces;

namespace Showdown3.StateMachine.PluginState;

public class StateOff : IState
{
    public StateOff(IContext context)
    {
        Context = context;
    }

    public IContext Context { get; }

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
        Context.TransitionTo(new StateOn(Context));
    }
}