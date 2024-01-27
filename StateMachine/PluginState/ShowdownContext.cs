using Showdown3.StateMachine.Interfaces;

namespace Showdown3.StateMachine.PluginState;

public class ShowdownContext : IContext
{
    public ShowdownContext()
    {
        TopContext = this;

        State = new StateOff(this);
        State.Enter();
    }

    public IState State { get; set; }
    public IContext TopContext { get; }
}