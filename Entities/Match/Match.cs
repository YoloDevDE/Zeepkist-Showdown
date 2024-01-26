using System.Collections.Generic;
using System.Threading.Tasks;
using Showdown3.Helper;
using Showdown3.Models;

namespace Showdown3.Entities.Match;

public class Match
{
    public Match(Team teamA, Team teamB)
    {
        TeamA = teamA;
        Initiative = TeamA;
        TeamB = teamB;
    }

    public Team TeamA { get; set; }
    public Team TeamB { get; set; }
    public Team Initiative { get; set; }

    public HashSet<Level> BannedMaps { get; set; } = new();

    public HashSet<Level> PickedMaps { get; set; } = new();

    public HashSet<Level> AvailableMaps { get; set; } = new();

    public override string ToString()
    {
        return $"{nameof(TeamA)}: {TeamA}, {nameof(TeamB)}: {TeamB}";
    }

    public List<Racer> GetAllRacersInCurrentMatch()
    {
        var tmp = new List<Racer>();
        tmp.AddRange(TeamA.Members);
        tmp.AddRange(TeamB.Members);
        return tmp;
    }

    public static async Task<Match> GetMatchFromServerAsync(string identifier)
    {
        var jsonResponse = await new HttpHelper().GetAsync($"/api/matches/{identifier}");
        return JsonHelper.DeserializeObject<Match>(jsonResponse);
    }
}