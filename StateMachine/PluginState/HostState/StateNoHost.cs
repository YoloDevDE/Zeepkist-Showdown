using Showdown3.StateMachine.Interfaces;
using ZeepkistClient;

namespace Showdown3.StateMachine.PluginState.HostState;

public class StateNoHost : IState
{
    public StateNoHost(IStateContext stateContext)
    {
        StateContext = stateContext;
    }

    public IStateContext StateContext { get; }


    public void Enter()
    {
        TaggedMessenger.Value.LogWarning("You have no Host. Mod is now in 'Passive' state");
        ZeepkistNetwork.MasterChanged += OnHostChanged;
        CheckHost();
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
        if (ZeepkistNetwork.LocalPlayerHasHostPowers()) StateContext.TransitionTo(new StateHost(StateContext));
    }
}