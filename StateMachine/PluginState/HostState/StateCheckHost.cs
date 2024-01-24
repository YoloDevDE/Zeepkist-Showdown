using Showdown3.StateMachine.Interfaces;
using ZeepkistClient;

namespace Showdown3.StateMachine.PluginState.HostState;

public class StateCheckHost : IState
{
    public StateCheckHost(IContext context)
    {
        Context = context;
    }

    public IContext Context { get; }

    public void Enter()
    {
        if (ZeepkistNetwork.LocalPlayerHasHostPowers())
            Context.TransitionTo(new StateIsHost(Context));
        else
            Context.TransitionTo(new StateIsNoHost(Context));
    }

    public void Exit()
    {
    }
}