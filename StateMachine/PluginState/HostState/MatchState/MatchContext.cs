﻿using Showdown3.Entities.Match;
using Showdown3.Models;
using Showdown3.StateMachine.Interfaces;

namespace Showdown3.StateMachine.PluginState.HostState.MatchState;

public class MatchContext : IContext
{
    public MatchContext(IContext topContext)
    {
        TopContext = topContext;
        State = new StateMatchRegister(this);
    }

    public Match Match { get; set; }

    public IState State { get; set; }
    public IContext TopContext { get; }
}