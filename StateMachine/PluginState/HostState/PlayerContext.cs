using Showdown3.StateMachine.Interfaces;

namespace Showdown3.StateMachine.PluginState.HostState;

public class PlayerContext : IContext
{
    public PlayerContext(IContext topContext)
    {
        TopContext = topContext;
        State = new StateCheckHost(this);
    }

    public IState State { get; set; }
    public IContext TopContext { get; }
}