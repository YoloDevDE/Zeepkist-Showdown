namespace Showdown3;

public class Score
{
    public Score(int position, double time)
    {
        Position = position;
        Time = time;
    }

    public double Time { get; }
    public int Position { get; }

    public int Points()
    {
        return Position switch
        {
            1 => 4,
            2 => 3,
            3 => 2,
            _ => 0
        };
    }
}