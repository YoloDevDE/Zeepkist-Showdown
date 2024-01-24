using System.Collections.Generic;

namespace Showdown3.Models;

public class RaceResult
{
    public RaceResult(Level raceLevel)
    {
        RaceLevel = raceLevel;
        RacerResults = new List<RacerResult>();
        TeamScores = new Dictionary<Team, int>();
        RacerPoints = new Dictionary<Racer, int>();
    }

    public Level RaceLevel { get; }
    public List<RacerResult> RacerResults { get; }
    public Dictionary<Team, int> TeamScores { get; private set; }
    public Dictionary<Racer, int> RacerPoints { get; private set; }
    public Team WinningTeam { get; }

    public void CalculateTeamScores()
    {
        // Berechnen Sie die Gesamtpunktzahl für jedes Team
        // und aktualisieren Sie TeamScores und WinningTeam entsprechend.
    }

    public void CalculateRacerPoints()
    {
        // Berechnen Sie die Gesamtpunktzahl für jeden einzelnen Racer
        // und aktualisieren Sie RacerPoints entsprechend.
    }

    // Weitere Methoden nach Bedarf...
}