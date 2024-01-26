using Showdown3.Commands;
using Showdown3.Entities.Match;
using Showdown3.Models;
using Showdown3.StateMachine.Interfaces;

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
        CommandStartMatch.OnHandle += OnCommandContinue;

        ((MatchContext)Context).Match = new Match(new Team("A", "A"), new Team("B", "B"));
        LobbyConfigurer.HoFLobby();
        new ServerMessageBuilder()
            .SetColor(Color.white)
            .AddText("Waiting for Host to Start the Match")
            .BuildAndExecute();
    }

    public void Exit()
    {
        CommandStartMatch.OnHandle -= OnCommandContinue;
    }

    private void OnCommandContinue()
    {
        Context.TransitionTo(new StatePreDraft(Context));
    }
}