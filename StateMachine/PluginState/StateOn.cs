using Showdown3.Commands;
using Showdown3.StateMachine.Interfaces;
using Showdown3.StateMachine.PluginState.HostState;
using ZeepSDK.Multiplayer;
using ZeepSDK.Racing;

namespace Showdown3.StateMachine.PluginState;

public class StateOn : IStateGateway
{
    public StateOn(IContext context)
    {
        Context = context;
        SubContext = new PlayerContext(Context);
    }

    public IState State { get; set; }

    public IContext Context { get; }
    public IContext SubContext { get; }

    public void Enter()
    {
        CommandStart.OnHandle += OnCommandStart;
        CommandStop.OnHandle += OnCommandStop;
        RacingApi.LevelLoaded += EnterPhotomode;
        MultiplayerApi.JoinedRoom += EnterPhotomode;

        EnterPhotomode();
        TaggedMessenger.Value.LogSuccess("started");
        SubContext.State.Enter();
    }


    public void Exit()
    {
        SubContext.State.Exit();
        CommandStart.OnHandle -= OnCommandStart;
        CommandStop.OnHandle -= OnCommandStop;
        RacingApi.LevelLoaded -= EnterPhotomode;
        MultiplayerApi.JoinedRoom -= EnterPhotomode;
        TaggedMessenger.Value.LogSuccess("stopped");
    }

    private void OnCommandStop()
    {
        Context.TransitionTo(new StateOff(Context));
    }

    private void OnCommandStart()
    {
        TaggedMessenger.Value.LogWarning("already started");
    }

    private void EnterPhotomode()
    {
        BehaviorHelper.EnterPhotomode();
    }
}