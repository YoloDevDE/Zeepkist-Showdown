using Showdown3.Commands;
using Showdown3.StateMachine.Interfaces;

namespace Showdown3.StateMachine.PluginState.HostState.MatchState;

public class StateMatchRegister : IState
{
    public StateMatchRegister(IContext context)
    {
        Context = context;
    }

    public IContext Context { get; }

    public void Enter()
    {
        CommandSetMatch.OnHandle += SetMatch;
    }

    private void SetMatch(string identifier)
    {
        
    }

    public void Exit()
    {
        CommandSetMatch.OnHandle -= SetMatch;
    }
}