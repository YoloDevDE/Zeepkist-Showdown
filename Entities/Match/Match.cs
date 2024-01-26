using Showdown3.Models;

namespace Showdown3.Entities.Match;

public class Match
{
    public Match(Team teamA, Team teamB)
    {
        TeamA = teamA;
        TeamB = teamB;
    }

    public Team TeamA { get; }
    public Team TeamB { get; }
}