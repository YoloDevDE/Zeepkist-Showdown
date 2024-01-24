using Showdown3.Models;
using Showdown3.StateMachine.Interfaces;
using ZeepSDK.Chat;

namespace Showdown3.StateMachine.PluginState.HostState.MatchState;

public class StateDraft : IState
{
    private Match _match;

    public StateDraft(IContext context)
    {
        Context = context;
        _match = ((MatchContext)Context).Match;
        ChatApi.SendMessage("yo Draft Constructor");
    }

    public IContext Context { get; }

    public void Enter()
    {
        // _countDown = new CountDown(5);
        // _countDown.Tick += CountDownOnTick;
        // _countDown.CountdownEnded += CountDownOnCountdownEnded;
        // ChatApi.SendMessage($"<br>Initiator: {_matchDraft.Initiator.Name}<br>Other Team:{_matchDraft.OtherTeam.Name}");
        ChatApi.SendMessage("Before Perform Draft");
        _match.PerformDraft();
        ChatApi.SendMessage("After Perform Draft");
    }

    public void Exit()
    {
        // _countDown.Tick -= CountDownOnTick;
        // _countDown.CountdownEnded -= CountDownOnCountdownEnded;
        // _countDown.End();
    }
    
}