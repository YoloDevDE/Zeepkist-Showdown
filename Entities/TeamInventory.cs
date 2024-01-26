using System.Collections.Generic;

namespace Showdown3.Models;

public class TeamInventory
{
    public TeamInventory(int maxBans = 2, int maxPicks = 1)
    {
        Bans = new HashSet<Level>(maxBans);
        Picks = new HashSet<Level>(maxPicks);
        MaxBans = maxBans;
        MaxPicks = maxPicks;
    }

    public HashSet<Level> Bans { get; }
    public HashSet<Level> Picks { get; }
    public int MaxBans { get; }
    public int MaxPicks { get; }

    public bool CanDraft()
    {
        return Bans.Count < MaxBans && Picks.Count < MaxPicks;
    }


    public bool TryAddBan(Level level)
    {
        if (Bans.Count < MaxBans) return Bans.Add(level);

        return false;
    }

    public bool TryAddPick(Level level)
    {
        if (Picks.Count < MaxPicks) return Picks.Add(level);

        return false;
    }

    public void RefreshInventory()
    {
        Bans.Clear();
        Picks.Clear();
    }

    // Weitere Methoden nach Bedarf...
}