using Showdown3.StateMachine.Interfaces;
using ZeepkistClient;

namespace Showdown3.StateMachine.PluginState.HostState;

public class StateIsNoHost : IState
{
    public StateIsNoHost(IContext context)
    {
        Context = context;
    }

    public IContext Context { get; }


    public void Enter()
    {
        ZeepkistNetwork.MasterChanged += OnHostChanged;

        TaggedMessenger.Value.LogWarning("You have no Host. Mod is now in 'Passive' state");
    }

    public void Exit()
    {
        ZeepkistNetwork.MasterChanged -= OnHostChanged;
    }

    private void OnHostChanged(ZeepkistNetworkPlayer zeepkistNetworkPlayer)
    {
        CheckHost();
    }

    private void CheckHost()
    {
        Context.TransitionTo(new StateCheckHost(Context));
    }
}