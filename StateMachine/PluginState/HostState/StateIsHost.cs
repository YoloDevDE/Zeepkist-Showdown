using Showdown3.Commands;
using Showdown3.StateMachine.Interfaces;
using Showdown3.StateMachine.PluginState.HostState.MatchState;
using ZeepkistClient;

namespace Showdown3.StateMachine.PluginState.HostState;

public class StateIsHost : IStateGateway
{
    public StateIsHost(IContext context)
    {
        Context = context;
        SubContext = new MatchContext(Context);
    }

    public IContext Context { get; }
    public IContext SubContext { get; set; }

    public void Enter()
    {
        ZeepkistNetwork.MasterChanged += OnHostChange;
        CommandMatchStop.OnHandle += StopMatchForcefully;
        TaggedMessenger.Value.LogSuccess("You have Host. Mod is now in 'Active' State");
        SubContext.State.Enter();
    }

    public void Exit()
    {
        SubContext.State.Exit();
        ZeepkistNetwork.MasterChanged -= OnHostChange;
        CommandMatchStop.OnHandle -= StopMatchForcefully;
    }

    public void StopMatchForcefully()
    {
        SubContext.State.Exit();

        SubContext = new MatchContext(Context);
        SubContext.State.Enter();
    }

    private void OnHostChange(ZeepkistNetworkPlayer zeepkistNetworkPlayer)
    {
        Context.TransitionTo(new StateCheckHost(Context));
    }
}