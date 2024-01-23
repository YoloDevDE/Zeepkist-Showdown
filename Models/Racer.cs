namespace Showdown3.Models;

public class Racer
{
    public Racer(string username, ulong steamId, Team team)
    {
        Username = username;
        SteamId = steamId;
        Team = team;
    }

    public Team Team { get; }

    public ulong SteamId { get; }
    public string Username { get; }
}