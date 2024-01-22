using Showdown3.StateMachine.Interfaces;
using Steamworks.ServerList;
using ZeepkistClient;

namespace Showdown3.StateMachine;

public class StateHasHost : IMasterState
{
    public StateHasHost(IStateMachine stateMachine)
    {
        StateMachine = stateMachine;
        CurrentState = new StateSyncWithServer(this);
    }

    public IStateMachine StateMachine { get; }

    public void Enter()
    {
        TaggedMessenger.Value.LogSuccess("You have Host. Mod is now in 'Active' State");
        CurrentState.Enter();
        ZeepkistNetwork.MasterChanged += OnHostChange;
    }

    public void Exit()
    {
        CurrentState.Exit();
        ZeepkistNetwork.MasterChanged -= OnHostChange;
    }

    public IState CurrentState { get; set; }

    private void OnHostChange(ZeepkistNetworkPlayer zeepkistNetworkPlayer)
    {
        if (!ZeepkistNetwork.LocalPlayerHasHostPowers()) StateMachine.TransitionTo(new StateHasNoHost(StateMachine));
    }
}