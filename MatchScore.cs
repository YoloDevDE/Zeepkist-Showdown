using Showdown3.Entities.Match;
using Showdown3.Models;

namespace Showdown3;

public class MatchScore
{
    public MatchScore(Match match)
    {
        Match = match;
    }

    public Match Match { get; }
}