using Showdown3.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Showdown3.Helper;
using ZeepSDK.Chat;

namespace Showdown3.Entities.Match;

public class Match
{
    public Match(Team teamA, Team teamB)
    {
        TeamA = teamA;
        TeamB = teamB;
        CurrentTurnTeam = TeamA; // Startet mit Team A
        BannedMaps = new HashSet<Level>();
        PickedMaps = new HashSet<Level>();
        AvailableMaps = new HashSet<Level>(); // Muss initialisiert oder zugewiesen werden
        Initiative = TeamA; // Oder basierend auf einem Münzwurf entscheiden
        Rounds = new List<Round>();
        MatchScores = new Dictionary<Team, int>
        {
            { TeamA, 0 },
            { TeamB, 0 }
        };
    }
    public void CalculateRoundWinnerAndUpdateMatchScore()
    {
        var currentRound = Rounds[RoundIndex];
        int teamAScore = currentRound.CalculateTeamScore(TeamA);
        int teamBScore = currentRound.CalculateTeamScore(TeamB);

        var roundWinner = teamAScore > teamBScore ? TeamA : teamBScore > teamAScore ? TeamB : null;
        if (roundWinner != null)
        {
            MatchScores[roundWinner]++;
        }

        RoundIndex++; // Gehe zur nächsten Runde
    }

    public int RoundIndex { get; set; } = 0;
    public List<Round> Rounds { get; set; }
    public Team TeamA { get; set; }
    public Team TeamB { get; set; }
    public Team CurrentTurnTeam { get; set; }

    public Dictionary<Team, int> MatchScores { get; private set; }
    public Team Initiative { get; private set; }

    public void SetInitiative(Team team)
    {
        Initiative = team;
        CurrentTurnTeam = team;
    }

    public HashSet<Level> BannedMaps { get; set; }
    public HashSet<Level> PickedMaps { get; set; }
    public HashSet<Level> AvailableMaps { get; set; }
    public Level LastDraftedLevel { get; set; }

    public bool MustPick { get; set; }
    public string LastDraftAction { get; set; }

    // Event, das aufgerufen wird, wenn der Draft abgeschlossen ist
    public event Action OnDraftCompleted;
    public event Action OnBanFailed;
    public event Action OnPickFailed;
    public event Action OnTeamSwapped;

    public event Action<Team> OnDraftSuccessful;

    public override string ToString()
    {
        return $"{nameof(TeamA)}: {TeamA}, {nameof(TeamB)}: {TeamB}";
    }

    public List<Racer> GetAllRacersInCurrentMatch()
    {
        var racers = new List<Racer>();
        racers.AddRange(TeamA.Members);
        racers.AddRange(TeamB.Members);
        return racers;
    }

    public void RefreshDraft()
    {
        foreach (var bannedMap in BannedMaps)
        {
            AvailableMaps.Add(bannedMap);
        }

        BannedMaps.Clear();
    }

    public void ExecutePick(int mapIndex)
    {
        if (mapIndex < 0 || mapIndex >= AvailableMaps.Count)
        {
            OnPickFailed?.Invoke();
            return;
        }

        var selectedLevel = AvailableMaps.ElementAt(mapIndex);

        if (!CurrentTurnTeam.Inventory.CanDraft())
        {
            PickedMaps.Add(SelectRandomMap());
        }
        else if (!CurrentTurnTeam.Inventory.TryAddPick(selectedLevel))
        {
            OnPickFailed?.Invoke();
            return;
        }
        else
        {
            PickedMaps.Add(selectedLevel);
            AvailableMaps.Remove(selectedLevel);
        }

        Rounds.Add(
            new Round(TeamA, TeamB, selectedLevel)
        );
        LastDraftAction = "picked";
        UpdateTurnTeamAndCheckDraftCompletion(selectedLevel);
    }

    public void ExecuteBan(int mapIndex)
    {
        if (mapIndex < 0 || mapIndex >= AvailableMaps.Count)
        {
            OnBanFailed?.Invoke();
            return;
        }

        var selectedLevel = AvailableMaps.ElementAt(mapIndex);

        if (!CurrentTurnTeam.Inventory.CanDraft())
        {
            BannedMaps.Add(SelectRandomMap());
        }
        else if (!CurrentTurnTeam.Inventory.TryAddBan(selectedLevel))
        {
            OnBanFailed?.Invoke();
            return;
        }
        else
        {
            BannedMaps.Add(selectedLevel);
            AvailableMaps.Remove(selectedLevel);
        }

        LastDraftAction = "banned";
        UpdateTurnTeamAndCheckDraftCompletion(selectedLevel);
    }


    private void UpdateTurnTeamAndCheckDraftCompletion(Level selectedLevel)
    {
        LastDraftedLevel = selectedLevel;
        UpdateTurnTeam();

        if (IsDraftCompleted())
        {
            OnDraftCompleted?.Invoke();
        }
    }


    private void UpdateTurnTeam()
    {
        OnDraftSuccessful?.Invoke(CurrentTurnTeam);
        CurrentTurnTeam = CurrentTurnTeam == TeamA ? TeamB : TeamA;
        OnTeamSwapped?.Invoke();
    }

    private bool IsDraftCompleted()
    {
        ChatApi.SendMessage($"{PickedMaps.Count}:{CurrentNeededMapCount}");
        return PickedMaps.Count == CurrentNeededMapCount;
    }

    public int CurrentNeededMapCount { get; set; } = 2;

    private Level SelectRandomMap()
    {
        var remainingMaps = AvailableMaps.Except(BannedMaps).Except(PickedMaps).ToList();
        return remainingMaps[new Random().Next(remainingMaps.Count)];
    }

    public static async Task<Match> GetMatchFromServerAsync(string identifier)
    {
        var jsonResponse = await new HttpHelper().GetAsync($"/api/matches/{identifier}");
        return JsonHelper.DeserializeObject<Match>(jsonResponse);
    }

    public void PickForRacer()
    {
        var count = AvailableMaps.Count;
        var rnd = new Random().Next(count);
        ExecutePick(rnd);
    }
}