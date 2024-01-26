namespace Showdown3.Models;

public class Racer
{
    public Racer(string name, ulong steamId, Team team = null)
    {
        Name = name;
        SteamId = steamId;
        Team = team;
    }

    public Team Team { get; set; }

    public ulong SteamId { get; }
    public string Name { get; }

    public override string ToString()
    {
        return $"{Name}";
    }
}