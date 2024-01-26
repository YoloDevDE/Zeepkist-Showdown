﻿using Showdown3.StateMachine.Interfaces;

namespace Showdown3.StateMachine.PluginState.HostState.MatchState;

public class StateWaitingForRacers : IState
{
    public StateWaitingForRacers(IContext context)
    {
        Context = context;
    }


    public void Enter()
    {
        Context.TransitionTo(new StateWaiting(Context));
    }

    public void Exit()
    {
    }

    public IContext Context { get; }
}