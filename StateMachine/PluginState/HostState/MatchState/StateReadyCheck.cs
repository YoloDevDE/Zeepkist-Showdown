using System;
using System.Collections.Generic;
using Showdown3.Commands;
using Showdown3.StateMachine.Interfaces;
using ZeepkistClient;
using ZeepSDK.Multiplayer;

namespace Showdown3.StateMachine.PluginState.HostState.MatchState;

public class StateReadyCheck : IState
{
    private CountDown _countDown;
    private HashSet<ulong> _readyVotes;
    private int _timeLeft;
    private int _votesMax;

    public StateReadyCheck(IStateContext stateContext)
    {
        StateContext = stateContext;
    }

    public IStateContext StateContext { get; }

    public void Enter()
    {
        _votesMax = ZeepkistNetwork.Players.Count;
        _readyVotes = new HashSet<ulong>();
        _countDown = new CountDown(60 * 20);
        _countDown.Tick += CountDownOnTick;
        _countDown.CountdownEnded += TransitionTo;
        CommandReady.OnHandle += OnCommandReady;
        MultiplayerApi.PlayerJoined += OnPlayerJoined;
        MultiplayerApi.PlayerLeft += OnPlayerLeft;

        new LobbyConfigurer()
            .SetTime(86400)
            .Build();
    }

    public void Exit()
    {
        _countDown.Tick -= CountDownOnTick;
        _countDown.CountdownEnded -= TransitionTo;
        CommandReady.OnHandle -= OnCommandReady;
        MultiplayerApi.PlayerJoined -= OnPlayerJoined;
        MultiplayerApi.PlayerLeft -= OnPlayerLeft;
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
            _countDown.End();
    }

    private void TransitionTo()
    {
        StateContext.TransitionTo(new StatePreRace(StateContext));
    }

    private void CountDownOnTick(int seconds)
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
            .AddText($"Time Left: {TimeSpan.FromSeconds(_timeLeft).Duration():mm':'ss}")
            .BuildAndExecute();
    }
}