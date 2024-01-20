using Showdown3.Commands;
using Showdown3.StateMachine.Contexts;
using Showdown3.StateMachine.States;
using ZeepkistClient;
using ZeepSDK.Chat;
using ZeepSDK.Messaging;

namespace Showdown3.StateMachine;

public class StateActive : IState
{
    public IContext Context { get; }
    public SlaveContext SlaveContext = new();

    public StateActive(IContext masterContext)
    {
        Context = masterContext;
    }


    public void Enter()
    {
        CommandStart.OnHandle += OnStart;
        CommandStop.OnHandle += OnStop;
        ZeepkistNetwork.MasterChanged += OnHostChanged;
        MessengerApi.LogSuccess("Showdown Started!");
        CheckIfLocalPlayerHasHost();
        if (ZeepkistNetwork.LocalPlayerHasHostPowers())
        {
            InitLobby();
        }
    }

    public void Exit()
    {
        CommandStart.OnHandle -= OnStart;
        CommandStop.OnHandle -= OnStop;
        ZeepkistNetwork.MasterChanged -= OnHostChanged;
    }

    public void CheckIfLocalPlayerHasHost()
    {
        if (ZeepkistNetwork.LocalPlayerHasHostPowers())
        {
            SlaveContext.SynchroniseStateWithServer();

            MessengerApi.LogSuccess("You are now the Host");
        }
        else
        {
            MessengerApi.LogWarning("You are not Host. If you get Host the mod will fallback.");
        }
    }

    public void OnHostChanged(ZeepkistNetworkPlayer zeepkistNetworkPlayer)
    {
        ChatApi.SendMessage($"Host changed -> {zeepkistNetworkPlayer.Username} is now Host");
        CheckIfLocalPlayerHasHost();
    }

    private void OnStop()
    {
        ChatApi.SendMessage("/joinmessage off");
        ChatApi.SendMessage("/servermessage remove");
        MessengerApi.LogSuccess("Showdown stopped!");
        SlaveContext.State.Exit();
        Context.SwitchState(new StateInActive(Context));
    }


    private void OnStart()
    {
        MessengerApi.LogError("Showdown is already running!");
    }

    private void InitLobby()
    {
        string joinmessage = "/joinmessage red ";
        joinmessage += new MessageBuilder()
            .AddSeparator()
            .AddLine("Welcome to the Zeepkist Showdown!")
            .AddSeparator()
            .AddLine("It's finally time - the moment we've all been waiting for!")
            .AddLine("Don't worry about a thing - we've got the details covered.")
            .AddLine("Keep your eyes on the race and ears open for our commands in the chat.")
            .AddLine("This is your moment. Race hard, follow our lead, and enjoy every second!")
            .AddLine("Good Luck, Have Fun!").Build();
        ChatApi.SendMessage(joinmessage);
        ChatApi.SendMessage("/joinmessage test");
        PlayerManager.Instance.currentMaster.flyingCamera.ToggleFlyingCamera();
    }
}