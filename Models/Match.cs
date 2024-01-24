using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Showdown3.Commands;
using ZeepSDK.Chat;

namespace Showdown3.Models;

public class Match
{
    private TaskCompletionSource<(ulong playerId, string levelName)> pickCompletionSource;

    public Match(Team teamA, Team teamB)
    {
        TeamA = teamA;
        TeamB = teamB;
        AvailableMaps = new HashSet<Level>(); // Alle verfügbaren Maps
        PickedMaps = new HashSet<Level>(); // Ausgewählte Maps für das Match
        BannedMaps = new HashSet<Level>(); // Gesperrte Maps
        RoundResults = new HashSet<RaceResult>();
        FirstPick = DetermineInitiative();
        LastPick = teamB;
        if (FirstPick == LastPick)
            LastPick = teamA;
    }

    public Team TeamA { get; }
    public Team TeamB { get; }
    public Team FirstPick { get; }
    public Team LastPick { get; }

    public HashSet<Level> AvailableMaps { get; }
    public HashSet<Level> PickedMaps { get; }
    public HashSet<Level> BannedMaps { get; }
    public HashSet<RaceResult> RoundResults { get; }

    // Methoden zur Verwaltung des Pick-and-Ban-Verfahrens
    public void PickMap(Level level)
    {
        /* ... */
    }

    public Team DetermineInitiative()
    {
        return new Random().Next(2) == 0 ? TeamA : TeamB;
    }

    private void HandlePickCommand(ulong playerId, string levelName)
    {
        pickCompletionSource?.TrySetResult((playerId, levelName));
    }

    private void HandleBanCommand(ulong playerId, string levelName)
    {
        
        pickCompletionSource?.TrySetResult((playerId, levelName));
    }

    private void SwapTeams(ref Team teamA, ref Team teamB)
    {
        (teamA, teamB) = (teamB, teamA);
    }


    public async Task PerformDraft()
    {
        CommandPick.OnHandle += HandlePickCommand;
        CommandPick.OnHandle += HandleBanCommand;
        Team currentPick = FirstPick;
        Team nextPick = LastPick;
        ChatApi.SendMessage("Perform Draft");
        while (TeamA.Inventory.CanDraft() || TeamB.Inventory.CanDraft())
        {
            if (PickedMaps.Count >= 2)
                break;
            MessageBuilder avaiableMaps = new MessageBuilder();
            foreach (Level level in AvailableMaps)
            {
                avaiableMaps.AddText(level.LevelName).AddBreak();
            }

            new MessageBuilder()
                .AddBreak()
                .AddText("Avaiable Maps:")
                .AddBreak()
                .AddText(avaiableMaps.Build())
                .AddSeparator()
                .AddText($"{currentPick.Tag} Use !pick or !ban")
                .BuildAndExecute();
            new ServerMessageBuilder()
                .SetColor(Color.yellow)
                .AddText(currentPick.GetFormattedInventory())
                .BuildAndExecute();

            // Initialisieren der TaskCompletionSource
            pickCompletionSource = new TaskCompletionSource<(ulong, string)>();

            // Warten auf Benutzereingabe
            var (playerId, pickedLevelName) = await pickCompletionSource.Task;

            // Verarbeiten der Eingabe
            ProcessDraft(playerId, pickedLevelName);
            
            SwapTeams(ref currentPick, ref nextPick);
        }

        CommandPick.OnHandle -= HandlePickCommand;
        CommandPick.OnHandle -= HandleBanCommand;
    }

    private void ProcessDraft(ulong playerId, string pickedLevelName)
    {
        // Logik, um die Auswahl zu verarbeiten
    }

    public void BanMap(Level level)
    {
        /* ... */
    }

    // Methode zur Berechnung der Punktzahlen und Ermittlung des Rundengewinners
    public void CalculateRoundScores()
    {
        /* ... */
    }

    // Weitere Methoden nach Bedarf...
}