using System.Collections.Generic;

namespace Showdown3.Entities;

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
        return CanPick() || CanBan();
    }

    public bool CanPick()
    {
        return Picks.Count < MaxPicks;
    }

    public bool CanBan()
    {
        return Bans.Count < MaxBans;
    }


    public bool TryAddBan(Level level)
    {
        return CanBan() && Bans.Add(level);
    }

    public bool TryAddPick(Level level)
    {
        return CanPick() && Picks.Add(level);
    }
}