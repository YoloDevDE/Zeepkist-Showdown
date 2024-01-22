using Showdown3.StateMachine.Interfaces;
using ZeepkistClient;

namespace Showdown3.StateMachine;

public class StateHasNoHost : IState
{
    public StateHasNoHost(IStateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public IStateMachine StateMachine { get; }


    public void Enter()
    {
        
        TaggedMessenger.Value.LogWarning("You have no Host. Mod is now in 'Passive' state");
        ZeepkistNetwork.MasterChanged += OnHostChanged;
        CheckHost();
    }

    private void OnHostChanged(ZeepkistNetworkPlayer zeepkistNetworkPlayer)
    {
  
        CheckHost();
    }

    private void CheckHost()
    {
        if (ZeepkistNetwork.LocalPlayerHasHostPowers())
        {
            StateMachine.TransitionTo(new StateHasHost(StateMachine));
        }
    }

    public void Exit()
    {
        ZeepkistNetwork.MasterChanged -= OnHostChanged;
    }
}