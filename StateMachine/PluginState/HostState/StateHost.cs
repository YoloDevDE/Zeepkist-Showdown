using Showdown3.StateMachine.Interfaces;
using Showdown3.StateMachine.PluginState.HostState.MatchState;
using UnityEngine;
using ZeepkistClient;

namespace Showdown3.StateMachine.PluginState.HostState;

public class StateHost : IStateContextConnector
{
    public Match Match { get; set; }

    public StateHost(IStateContext stateContext)
    {
        StateContext = stateContext;
        State = new StateWaiting(this);
    }

    public IStateContext StateContext { get; }

    public void Enter()
    {
        ZeepkistNetwork.MasterChanged += OnHostChange;
        TaggedMessenger.Value.LogSuccess("You have Host. Mod is now in 'Active' State");
        State.Enter();
    }

    public void Exit()
    {
        ZeepkistNetwork.MasterChanged -= OnHostChange;
        State.Exit();
    }

    public IState State { get; set; }

    private void OnHostChange(ZeepkistNetworkPlayer zeepkistNetworkPlayer)
    {
        if (!ZeepkistNetwork.LocalPlayerHasHostPowers()) StateContext.TransitionTo(new StateNoHost(StateContext));
    }
}