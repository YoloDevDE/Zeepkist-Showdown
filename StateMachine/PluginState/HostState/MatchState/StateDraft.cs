using Showdown3.Entities.Match;
using Showdown3.Models;
using Showdown3.StateMachine.Interfaces;

namespace Showdown3.StateMachine.PluginState.HostState.MatchState;

public class StateDraft : IState
{
    public StateDraft(IContext context)
    {
        Context = context;
    }


    public Match Match { get; }
    public Draft Draft { get; }


    public void Enter()
    {
        // _countDown = new CountDown(5);
        // _countDown.Tick += CountDownOnTick;
        // _countDown.CountdownEnded += CountDownOnCountdownEnded;
        // ChatApi.SendMessage($"<br>Initiator: {_matchDraft.Initiator.Name}<br>Other Team:{_matchDraft.OtherTeam.Name}");
    }

    public void Exit()
    {
        // _countDown.Tick -= CountDownOnTick;
        // _countDown.CountdownEnded -= CountDownOnCountdownEnded;
        // _countDown.End();
    }

    public IContext Context { get; }
}