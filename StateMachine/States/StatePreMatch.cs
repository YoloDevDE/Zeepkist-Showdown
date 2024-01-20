using System.Timers;
using Showdown3.StateMachine.Contexts;

namespace Showdown3.StateMachine.States;

public class StatePreMatch : IState
{
    public IContext Context { get; }
    private int _serverMessageIndex = 0;
    private Timer _timer;

    public StatePreMatch(IContext context)
    {
        Context = context;
    }


    public void Enter()
    {
        ServerMessage();
        StartTimer();
    }

    private void ServerMessage()
    {
        string dot = "";
        for (int i = 0; i <= _serverMessageIndex % 3; i++)
        {
            dot += ".";
        }
        

        // Berechne die Anzahl der Leerzeichen basierend auf der Länge von 'dot'
        var space = new string(' ', 3 - dot.Length);
        _serverMessageIndex++;
        new MessageBuilder()
            .AddLine($"{space}Waiting for Ready-Check{dot}")
            .BuildAndServermessage();
    }

    private void StartTimer()
    {
        _timer = new Timer(1000);
        _timer.Elapsed += ServerMessage;
        _timer.AutoReset = true;
        _timer.Enabled = true;
    }

    private void ServerMessage(object sender, ElapsedEventArgs e)
    {
        ServerMessage();
    }

    public void Exit()
    {
        _timer.Elapsed -= ServerMessage;
    }
}