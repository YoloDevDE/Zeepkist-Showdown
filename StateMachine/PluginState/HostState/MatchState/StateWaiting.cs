using Showdown3.Commands;
using Showdown3.StateMachine.Interfaces;

namespace Showdown3.StateMachine.PluginState.HostState.MatchState;

public class StateWaiting : IState
{
    public Match Match { get; set; }

    public StateWaiting(IStateContext stateContext)
    {
        StateContext = stateContext;
    }

    public IStateContext StateContext { get; }

    public void Enter()
    {
        CommandContinue.OnHandle += OnCommandContinue;
        LobbyConfigurer.HoFLobby();
        new ServerMessageBuilder()
            .SetColor(Color.white)
            .AddText("Waiting for Host to Start the Match")
            .BuildAndExecute();
        ((StateHost)StateContext).Match = new Match();
    }

    public void Exit()
    {
        CommandContinue.OnHandle -= OnCommandContinue;
    }

    private void OnCommandContinue()
    {
        StateContext.TransitionTo(new StateDraft(StateContext));
    }
}