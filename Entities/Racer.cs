namespace Showdown3.Models;

public class Racer
{
    public Racer(string username, ulong steamId, Team team = null)
    {
        Username = username;
        SteamId = steamId;
        Team = team;
    }

    public Team Team { get; set; }

    public ulong SteamId { get; }
    public string Username { get; }
}