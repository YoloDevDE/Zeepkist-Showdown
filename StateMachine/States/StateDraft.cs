using System;
using System.Collections.Generic;
using System.Timers;
using Showdown3.Commands;
using Showdown3.Models;
using Showdown3.StateMachine.Contexts;
using ZeepkistClient;
using ZeepSDK.Chat;

namespace Showdown3.StateMachine.States;

public class StateDraft : IState
{
    private readonly int _draftTime = 120;
    private int _draftTimeCurrent = 120;
    private int _teamIndex;
    private readonly List<Team> _teams = new();
    public Timer Timer { get; private set; }
    public IContext Context { get; }

    public StateDraft(IContext context)
    {
        Context = context;
    }


    public void Enter()
    {
        CommandBan.OnHandle += OnDraft;
    }

    public void Exit()
    {
        Timer.Elapsed -= Update;
        CommandBan.OnHandle -= OnDraft;
    }

    private void OnDraft(ulong steamId, string arguments)
    {
        SwitchTeam();
    }

    private void Update(object sender, ElapsedEventArgs e)
    {
        UpdateServerMessage();
        _draftTimeCurrent--;
    }

    private void UpdateServerMessage()
    {
        var timeSpan = TimeSpan.FromSeconds(_draftTimeCurrent);
        var timeFormatted = timeSpan.ToString(@"mm\:ss");
        var i = _teamIndex % 2;
        new MessageBuilder()
            .AddLine($"Its [{_teams[i].Tag}] {_teams[i].Name} turn!")
            .AddLine($"-> {timeFormatted} left")
            .BuildAndServermessage();
    }

    private void StartTimer()
    {
        Update(null, null);
        Timer = new Timer(1000);
        Timer.Elapsed += Update;
        Timer.AutoReset = true;
        Timer.Enabled = true;
    }

    private void ReStartTimer()
    {
        Timer.Elapsed -= Update;
        StartTimer();
    }

    private void SwitchTeam()
    {
        var i = _teamIndex % 2;
        new MessageBuilder()
            .AddLine($"[{_teams[i].Tag}] {_teams[i].Name} picked xyz")
            .BuildAndSend();
        _draftTimeCurrent = _draftTime;
        _teamIndex++;
        UpdateServerMessage();
        ReStartTimer();
        if (_teamIndex >= 4)
        {
            Context.SwitchState(new StatePreMatch(Context));
        }
    }
}