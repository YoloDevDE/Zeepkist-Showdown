using Showdown3.Commands;
using Showdown3.Models;
using Showdown3.StateMachine.Interfaces;
using ZeepSDK.Chat;

namespace Showdown3.StateMachine.PluginState.HostState.MatchState;

public class StateWaiting : IState
{
    public StateWaiting(IContext context)
    {
        Context = context;
    }


    public IContext Context { get; }

    public void Enter()
    {
        CommandContinue.OnHandle += OnCommandContinue;
        
        ((MatchContext)Context).Match = new Match(new Team("A", "A"), new Team("B", "B"));
        LobbyConfigurer.HoFLobby();
        new ServerMessageBuilder()
            .SetColor(Color.white)
            .AddText("Waiting for Host to Start the Match")
            .BuildAndExecute();
        ChatApi.SendMessage("yo1");
    }

    public void Exit()
    {
        CommandContinue.OnHandle -= OnCommandContinue;
    }

    private void OnCommandContinue()
    {
        ChatApi.SendMessage("yo2");
        Context.TransitionTo(new StateDraft(Context));
    }
}