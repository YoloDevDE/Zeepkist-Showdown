﻿using System;
using System.Collections.Generic;
using Showdown3.Commands;
using Showdown3.Entities.Match;
using Showdown3.Helper;
using Showdown3.StateMachine.Interfaces;
using ZeepkistClient;
using ZeepSDK.Multiplayer;

namespace Showdown3.StateMachine.PluginState.HostState.MatchState;

public class StateReadyCheck : IState
{
    private Countdown _countdown;
    private HashSet<ulong> _readyVotes;
    private int _timeLeft;
    private int _votesMax;
    private Match _match;

    public StateReadyCheck(IContext context)
    {
        Context = context;
        _match =
            ((MatchContext)Context).Match;
    }

    public IContext Context { get; }

    public void Enter()
    {
        _votesMax = ZeepkistNetwork.Players.Count;
        _readyVotes = new HashSet<ulong>();
        _countdown = new Countdown(60 * 20);
        _countdown.OnTick += CountdownOnTick;
        _countdown.OnCountdownEnd += TransitionTo;
        CommandReady.OnHandle += OnCommandReady;
        MultiplayerApi.PlayerJoined += OnPlayerJoined;
        MultiplayerApi.PlayerLeft += OnPlayerLeft;
        _countdown.Start();
        new LobbyConfigurer()
            .SetTime(86400)
            .Build();
    }

    public void Exit()
    {
        _countdown.OnTick -= CountdownOnTick;
        _countdown.OnCountdownEnd -= TransitionTo;
        CommandReady.OnHandle -= OnCommandReady;
        MultiplayerApi.PlayerJoined -= OnPlayerJoined;
        MultiplayerApi.PlayerLeft -= OnPlayerLeft;
        ((MatchContext)Context).Match = _match;
        _countdown.Stop();
    }

    private void OnPlayerLeft(ZeepkistNetworkPlayer player)
    {
        _readyVotes.Remove(player.SteamID);
        UpdateServerMessage();
    }

    private void OnPlayerJoined(ZeepkistNetworkPlayer player)
    {
        _votesMax = ZeepkistNetwork.Players.Count;
        UpdateServerMessage();
    }

    private void OnCommandReady(ulong steamId, string argument)
    {
        _readyVotes.Add(steamId);
        UpdateServerMessage();
        if (_readyVotes.Count == _votesMax)
            TransitionTo();
    }

    private void TransitionTo()
    {
        Context.TransitionTo(new StatePreRace(Context));
    }

    private void CountdownOnTick(int seconds)
    {
        _timeLeft = seconds;
        UpdateServerMessage();
    }

    private void UpdateServerMessage()
    {
        new ServerMessageBuilder()
            .SetColor(Color.red)
            .AddText($"!ready ({_readyVotes.Count}/{_votesMax})")
            .AddBreak()
            .AddText($"Time Left: {_countdown.GetFormattedRemainingTime()}")
            .BuildAndExecute();
    }
}