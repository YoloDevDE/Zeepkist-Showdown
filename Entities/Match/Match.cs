using Showdown3.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Showdown3.Helper;

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
    }

    public Team TeamA { get; set; }
    public Team TeamB { get; set; }
    public Team CurrentTurnTeam { get; set; }
    public Team Initiative { get; set; }
    public HashSet<Level> BannedMaps { get; set; }
    public HashSet<Level> PickedMaps { get; set; }
    public HashSet<Level> AvailableMaps { get; set; }

    // Event, das aufgerufen wird, wenn der Draft abgeschlossen ist
    public event Action OnDraftCompleted;
    public event Action OnBanFailed;
    public event Action OnPickFailed;

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

        UpdateTurnTeamAndCheckDraftCompletion();
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

        UpdateTurnTeamAndCheckDraftCompletion();
    }


    private void UpdateTurnTeamAndCheckDraftCompletion()
    {
        UpdateTurnTeam();
        if (IsDraftCompleted())
        {
            OnDraftCompleted?.Invoke();
        }
    }
    


    private void UpdateTurnTeam()
    {
        CurrentTurnTeam = CurrentTurnTeam == TeamA ? TeamB : TeamA;
    }

    private bool IsDraftCompleted()
    {
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
}