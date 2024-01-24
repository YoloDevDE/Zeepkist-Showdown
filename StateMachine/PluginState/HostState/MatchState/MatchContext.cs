using Showdown3.Models;
using Showdown3.StateMachine.Interfaces;

namespace Showdown3.StateMachine.PluginState.HostState.MatchState;

public class MatchContext : IContext
{
    public Match Match { get; set; }

    public MatchContext(IContext topContext)
    {
        TopContext = topContext;
        State = new StateWaitingForRacers(this);
    }

    public IState State { get; set; }
    public IContext TopContext { get; }
}