using Showdown3.Commands;
using Showdown3.Entities.Match;
using Showdown3.Models;
using Showdown3.StateMachine.Interfaces;
using ZeepSDK.Chat;

namespace Showdown3.StateMachine.PluginState.HostState.MatchState;

public class StatePreDraft : IState
{
    public StatePreDraft(IContext context)
    {
        Context = context;
    }


    public Match Match { get; set; }
    public Team Initiative { get; set; }


    public void Enter()
    {
        CommandSetInitiative.OnHandle += OnCommandSetInitiative;

        Match = ((MatchContext)Context).Match;
    }

    public void Exit()
    {
    }

    public IContext Context { get; }

    private void OnCommandSetInitiative(string arguments)
    {
        switch (arguments.ToUpper())
        {
            case "A":
                break;
            case "B":
                break;
            default:
                ChatApi.AddLocalMessage("Invalid arguments. Use \"A\" or \"B\"");
                break;
        }
    }
}