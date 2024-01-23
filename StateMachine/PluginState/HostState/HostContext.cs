using Showdown3.StateMachine.Interfaces;
using ZeepkistClient;
using ZeepSDK.Multiplayer;
using ZeepSDK.Racing;

namespace Showdown3.StateMachine.PluginState.HostState;

public class HostContext : IStateContextConnector
{
    public HostContext(IStateContext stateContext)
    {
        StateContext = stateContext;
        State = ZeepkistNetwork.LocalPlayerHasHostPowers()
            ? new StateHost(this)
            : new StateNoHost(this);
    }




    public IStateContext StateContext { get; }

    public void Enter()
    {
        EnterPhotomode();
        RacingApi.PlayerSpawned += EnterPhotomode;
        MultiplayerApi.JoinedRoom += EnterPhotomode;

        State.Enter();
    }

    public void Exit()
    {
        RacingApi.PlayerSpawned -= EnterPhotomode;
        MultiplayerApi.JoinedRoom -= EnterPhotomode;

        State.Exit();
    }

    public IState State { get; set; }

    private void EnterPhotomode()
    {
        BehaviorHelper.EnterPhotomode();
    }
}