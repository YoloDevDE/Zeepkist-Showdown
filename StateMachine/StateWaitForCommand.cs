using Showdown3.Commands;
using Showdown3.StateMachine.Interfaces;

namespace Showdown3.StateMachine;

public class StateWaitForCommand : IState
{
    public StateWaitForCommand(IStateMachine stateMachine)
    {
        StateMachine = stateMachine;
        Context = stateMachine as StateShowdownMain;
    }

    public IStateMachine StateMachine { get; }
    private StateShowdownMain Context { get; set; }

    public void Enter()
    {
        Context.Match = new Match(null, null);
        CommandContinue.OnHandle += OnCommandContinue;
        LobbyConfigurer.HoFLobby();
        new ServerMessageBuilder()
            .SetColor(Color.white)
            .AddText("Waiting for Host")
            .BuildAndExecute();
    }

    public void Exit()
    {
        CommandContinue.OnHandle -= OnCommandContinue;
    }

    private void OnCommandContinue()
    {
        StateMachine.TransitionTo(new StateDraft(StateMachine));
    }
}