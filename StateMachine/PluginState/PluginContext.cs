using Showdown3.StateMachine.Interfaces;

namespace Showdown3.StateMachine.PluginState;

public class PluginContext : IStateContext
{
    public PluginContext()
    {
        State = new StateOff(this);
        State.Enter();
    }

    public IState State { get; set; }
}