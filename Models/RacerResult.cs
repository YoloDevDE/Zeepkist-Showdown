namespace Showdown3.Models;

public class RacerResult
{
    public RacerResult(Racer racer, int placement, int points)
    {
        Racer = racer;
        Placement = placement;
        Points = points;
    }

    public Racer Racer { get; }
    public int Placement { get; }
    public int Points { get; }
}