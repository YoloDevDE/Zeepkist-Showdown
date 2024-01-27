using Showdown3.Commands;
using Showdown3.Entities;
using Showdown3.Entities.Match;
using Showdown3.Helper;
using Showdown3.StateMachine.Interfaces;
using ZeepSDK.Chat;

namespace Showdown3.StateMachine.PluginState.HostState.MatchState;

public class StatePreDraft : IState
{
    private Match _match;

    public StatePreDraft(IContext context)
    {
        Context = context;
    }

    public Team Initiative { get; set; }


    public void Enter()
    {
        CommandMatchInitiative.OnHandle += OnCommandSetInitiative;

        _match = ((MatchContext)Context).Match;

        new ServerMessageBuilder()
            .SetColor(Color.white)
            .AddText("Choosing the initiative team...")
            .BuildAndExecute();
    }

    public void Exit()
    {
        CommandMatchInitiative.OnHandle -= OnCommandSetInitiative;
        ((MatchContext)Context).Match = _match;
    }

    public IContext Context { get; }

    private void OnCommandSetInitiative(string arguments)
    {
        switch (arguments.ToUpper())
        {
            case "A":
                _match.SetInitiative(_match.TeamA);
                break;
            case "B":
                _match.SetInitiative(_match.TeamB);
                break;
            default:
                ChatApi.AddLocalMessage("Invalid arguments. Use \"A\" or \"B\"");
                return;
        }

        ChatApi.SendMessage($"Initiative is set to {_match.Initiative.Tag}");
        Context.TransitionTo(new StateDraft(Context));
    }
}