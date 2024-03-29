﻿using System.Collections.Generic;
using System.Linq;
using Showdown3.Helper;

namespace Showdown3.Entities;

public class Team
{
    public Team(string name, string tag, ulong challongeId = default)
    {
        Members = new HashSet<Racer>();
        Name = name;
        Tag = $"[{tag}]";
        ChallongeId = challongeId;
        Inventory = new TeamInventory();
    }

    public HashSet<Racer> Members { get; set; }
    public string Name { get; set; }
    public string Tag { get; set; }
    public ulong ChallongeId { get; set; }
    public TeamInventory Inventory { get; }


    public Racer GetRacerBySteamId(ulong steamId)
    {
        foreach (Racer racer in Members)
        {
            if (racer.SteamId == steamId)
                return racer;
        }

        return null;
    }

    public override string ToString()
    {
        return $"{Tag} {Name}: {string.Join(",", Members)}";
    }

    public string GetFormattedInventory()
    {
        var formattedBans = Inventory.Bans.Count > 0
            ? string.Join(", ", Inventory.Bans.Select(b => b.Name))
            : "None";
        var formattedPicks = Inventory.Picks.Count > 0
            ? string.Join(", ", Inventory.Picks.Select(p => p.Name))
            : "None";
        string tmpName = Name;
        if (Name.Length > 31)
            tmpName = Name[..32] + "...";
        return new MessageBuilder()
            .AddText($"{Tag} {tmpName}")
            .AddBreak()
            .AddSeparator()
            .AddText($"Banned: {formattedBans} | Picked: {formattedPicks}")
            .AddBreak()
            .AddText(
                $"Bans Left: {Inventory.MaxBans - Inventory.Bans.Count} | Picks Left: {Inventory.MaxPicks - Inventory.Picks.Count}")
            .Build();
    }
}