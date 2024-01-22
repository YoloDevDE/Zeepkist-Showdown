using Showdown3.Models;

namespace Showdown3;

public class MatchScore
{
    public Match Match { get; }

    public MatchScore(Match match)
    {
        Match = match;
    }
}