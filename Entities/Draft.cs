using System.Collections.Generic;
using System.Threading.Tasks;
using Showdown3.Entities.Match;

namespace Showdown3.Models;

public class Draft
{
    public Draft(Match match)
    {
        Match = match;
        TeamA = match.TeamA;
        TeamB = match.TeamB;
        AvailableMaps = new HashSet<Level>();
        PickedMaps = new HashSet<Level>();
        BannedMaps = new HashSet<Level>();
    }

    public Team TeamA { get; set; }
    public Team TeamB { get; set; }

    public Match Match { get; }

    public HashSet<Level> BannedMaps { get; set; }

    public HashSet<Level> PickedMaps { get; set; }

    public HashSet<Level> AvailableMaps { get; set; }

    private void SwapTeams(ref Team teamA, ref Team teamB)
    {
        (teamA, teamB) = (teamB, teamA);
    }

    public async Task PerformDraft()
    {
    }
}