using System.Collections.Generic;
using System.Linq;

namespace Showdown3.Models;

public class Team
{
    public Team(string name, string tag, ulong challongeId = default)
    {
        Racers = new HashSet<Racer>();
        Name = name;
        Tag = $"[{tag}]";
        ChallongeId = challongeId;
        Inventory = new TeamInventory();
    }

    public HashSet<Racer> Racers { get; set; }
    public string Name { get; set; }
    public string Tag { get; set; }
    public ulong ChallongeId { get; set; }
    public TeamInventory Inventory { get; private set; }

    public string GetFormattedInventory()
    {
        string formattedBans = Inventory.Bans.Count > 0
            ? string.Join(", ", Inventory.Bans.Select(b => b.LevelName))
            : "None";
        string formattedPicks = Inventory.Picks.Count > 0
            ? string.Join(", ", Inventory.Picks.Select(p => p.LevelName))
            : "None";

        return new MessageBuilder()
            .AddText($"{Tag} {Name}")
            .AddBreak()
            .AddSeparator()
            .AddText($"Banned: {formattedBans} | Picked: {formattedPicks}")
            .AddBreak()
            .AddText(
                $"Bans Left: {Inventory.MaxBans - Inventory.Bans.Count} | Picks Left: {Inventory.MaxPicks - Inventory.Picks.Count}")
            .Build();
    }
}