using System.Linq;
using Showdown3.Commands;
using Showdown3.Entities;
using Showdown3.Entities.Match;
using Showdown3.Helper;
using Showdown3.StateMachine.Interfaces;
using ZeepSDK.Chat;

namespace Showdown3.StateMachine.PluginState.HostState.MatchState;

public class StateDraft : IState
{
    private const int DecisionTimeLimit = 10 * 60; // 10 Minuten in Sekunden
    private readonly Match _match;
    private bool _mustPick = false;
    private Countdown _countdown = new(DecisionTimeLimit);

    public StateDraft(IContext context)
    {
        Context = context;
        _match = ((MatchContext)context).Match;
    }

    public Match Match { get; }

    public void Enter()
    {
        CommandBan.OnHandle += BanMap;
        CommandPick.OnHandle += PickMap;
        _match.OnDraftSuccessful += PrintDraftSuccessful;
        _match.OnDraftCompleted += DraftCompleted;
        _match.OnBanFailed += PrintBanFailed;
        _match.OnPickFailed += PrintPickFailed;
        _match.OnTeamSwapped += PrintDraftText;
        _countdown.OnTick += Update;
        _countdown.OnCountdownEnd += ApplyPenalty;

        _countdown.Start();
        ChatApi.SendMessage("Drafting started!");
        PrintDraftText();
    }

    private void PrintPickFailed()
    {
    }

    private void PrintBanFailed()
    {
        new MessageBuilder()
            .AddText($"{_match.CurrentTurnTeam.Tag} You have no more Bans left!")
            .BuildAndExecute();
    }

    private void ApplyPenalty()
    {
        _match.PickForRacer();
    }

    private void Update(int seconds)
    {
        new ServerMessageBuilder()
            .SetColor(Color.red)
            .AddText("Drafting".ToUpper())
            .AddBreak()
            .AddText($"Time Left: {_countdown.GetFormattedRemainingTime()}")
            .BuildAndExecute();
    }

    private void PrintDraftSuccessful(Team team)
    {
        new MessageBuilder()
            .AddBreak()
            .AddText($"{team.Tag} {_match.LastDraftAction.ToUpper()} {_match.LastDraftedLevel}")
            .BuildAndExecute();
        new MessageBuilder()
            .AddBreak()
            .AddText(team.GetFormattedInventory())
            .BuildAndExecute();
        _countdown.Reset();
    }

    private void PrintDraftText()
    {
        string avaiableMaps = "";
        for (var i = 0; i < _match.AvailableMaps.Count; i++)
        {
            avaiableMaps += $"{i}: {_match.AvailableMaps.ToList()[i].Name}<br>";
        }

        new MessageBuilder()
            .AddBreak()
            .AddText(_match.CurrentTurnTeam.GetFormattedInventory())
            .AddBreak()
            .AddText($"Avaiable Maps:")
            .AddBreak()
            .AddText(avaiableMaps)
            .AddSeparator()
            .AddText($"{_match.CurrentTurnTeam.Tag} -> Use '!pick number' or '!ban number'")
            .BuildAndExecute();
    }


    public void Exit()
    {
        CommandBan.OnHandle -= BanMap;
        CommandPick.OnHandle -= PickMap;
        _match.OnDraftSuccessful -= PrintDraftSuccessful;
        _match.OnDraftCompleted -= DraftCompleted;
        _match.OnBanFailed -= PrintBanFailed;
        _match.OnPickFailed -= PrintPickFailed;
        _match.OnTeamSwapped -= PrintDraftText;
        _countdown.OnTick -= Update;
        _countdown.OnCountdownEnd -= ApplyPenalty;
        _countdown.Stop();
        ((MatchContext)Context).Match = _match;
    }


    public IContext Context { get; }

    private void DraftCompleted()
    {
        Context.TransitionTo(new StateReadyCheck(Context));
    }

    private void BanMap(ulong steamId, string argument)
    {
        // if (_match.CurrentTurnTeam.GetRacerBySteamId(steamId) == null)
        // {
        //     ChatApi.SendMessage("Not your turn!!!");
        //     return;
        // }
        if (_mustPick)
        {
            ChatApi.SendMessage("You MUST use !pick now cause the other team picked.");
            return;
        }

        if (!int.TryParse(argument, out var mapIndex) || !IsValidMapIndex(mapIndex))
        {
            ChatApi.SendMessage("Invalid map selection.");
            return;
        }

        _match.ExecuteBan(mapIndex);
    }

    private void PickMap(ulong steamId, string argument)
    {
        // if (_match.CurrentTurnTeam.GetRacerBySteamId(steamId) == null)
        // {
        //     ChatApi.SendMessage("Not your turn!!!");
        //     return;
        // }

        if (!int.TryParse(argument, out var mapIndex) || !IsValidMapIndex(mapIndex))
        {
            ChatApi.SendMessage("Invalid map selection.");
            return;
        }

        _mustPick = true;
        _match.ExecutePick(mapIndex);
    }

    private bool IsValidMapIndex(int index)
    {
        return index >= 0 && index < _match.AvailableMaps.Count;
    }
}