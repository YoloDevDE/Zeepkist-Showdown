﻿using Showdown3.StateMachine.Interfaces;

namespace Showdown3.StateMachine.PluginState.HostState.MatchState;

public class StateCheckResult : IState
{
    public StateCheckResult(IContext context)
    {
        Context = context;
    }

    public IContext Context { get; }

    public void Enter()
    {
    }

    public void Exit()
    {
    }
}