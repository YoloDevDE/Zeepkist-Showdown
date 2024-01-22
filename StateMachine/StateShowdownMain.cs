using Showdown3.Models;
using Showdown3.StateMachine.Interfaces;
using ZeepkistClient;
using ZeepSDK.Multiplayer;
using ZeepSDK.Racing;

namespace Showdown3.StateMachine;

public class StateShowdownMain : IMasterState
{

    public Match Match { get; set; }

    public StateShowdownMain(IStateMachine stateMachine)
    {
        StateMachine = stateMachine;
        CurrentState = ZeepkistNetwork.LocalPlayerHasHostPowers() ? new StateHasHost(this) : new StateHasNoHost(this);
    }


    public IStateMachine StateMachine { get; }

    public void Enter()
    {

        CurrentState.Enter();
        EnterPhotomode();
        RacingApi.RoundStarted += EnterPhotomode;
        MultiplayerApi.JoinedRoom += EnterPhotomode;
    }

    private void EnterPhotomode()
    {
        BehaviorHelper.EnterPhotomode();
    }

    public void Exit()
    {
        
        CurrentState.Exit();
        RacingApi.RoundStarted -= EnterPhotomode;
        MultiplayerApi.JoinedRoom -= EnterPhotomode;
    }

    public IState CurrentState { get; set; }
}