using Showdown3.Entities;
using Showdown3.Entities.Match;
using Showdown3.StateMachine.Interfaces;

namespace Showdown3.StateMachine.PluginState.HostState.MatchState;

using System;
using System.Threading;
using Showdown3.Entities;
using Showdown3.Entities.Match;
using Showdown3.StateMachine.Interfaces;

public class StateDraft : IState
{
    private Timer _decisionTimer;
    private const int DecisionTimeLimit = 10 * 60 * 1000; // 10 Minuten in Millisekunden

    public StateDraft(IContext context)
    {
        Context = context;
        _decisionTimer = new Timer(OnDecisionTimeElapsed, null, Timeout.Infinite, Timeout.Infinite);
    }

    public Match Match { get; private set; }

    public void Enter()
    {
        StartDecisionTimer();
        // Weitere Initialisierung...
    }

    public void Exit()
    {
        _decisionTimer?.Change(Timeout.Infinite, Timeout.Infinite);
        // Weitere Bereinigungslogik...
    }

    private void StartDecisionTimer()
    {
        _decisionTimer?.Change(DecisionTimeLimit, Timeout.Infinite);
    }

    private void OnDecisionTimeElapsed(object state)
    {
        // Logik, die ausgeführt wird, wenn die Zeit abgelaufen ist
        // Zum Beispiel: Automatischer Ban/Pick oder Zug überspringen
    }

    public IContext Context { get; private set; }

    // Weitere Methoden und Logik...
}
