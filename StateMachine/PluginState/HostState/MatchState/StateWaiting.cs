using Showdown3.Commands;
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
        CommandMatchStart.OnHandle += MatchStart;


        new ServerMessageBuilder()
            .SetColor(Color.white)
            .AddText("Waiting for Host to Start the Match")
            .BuildAndExecute();
    }

    public void Exit()
    {
        CommandMatchStart.OnHandle -= MatchStart;
    }

    private void MatchStart()
    {
        Context.TransitionTo(new StatePreDraft(Context));
    }
}