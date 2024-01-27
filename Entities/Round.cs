using System.Collections.Generic;
using System.Linq;
using ZeepkistClient;
using ZeepSDK.Chat;
using ZeepSDK.Playlist;
using ZeepSDK.Workshop;

namespace Showdown3.Entities;

public class Round
{
    public Team TeamA { get; set; }
    public Team TeamB { get; set; }
    public Level Level { get; set; }
    public int LevelIndex { get; set; }

    public Round(Team teamA, Team teamB, Level level)
    {
        TeamA = teamA;
        TeamB = teamB;
        Level = level;
        LevelIndex = 0;
        SetLevelIndex(Level);
    }

    public Dictionary<Racer, int> RacerScores { get; private set; } = new Dictionary<Racer, int>();

    private int GetPointsForPosition(int position)
    {
        switch (position)
        {
            case 1: return 4; // 1st place
            case 2: return 3; // 2nd place
            case 3: return 2; // 3rd place
            default: return 0; // 4th place, DNF, or below
        }
    }

    public void UpdateRacerScore(Racer racer, int position)
    {
        RacerScores[racer] = GetPointsForPosition(position);
    }

    public int CalculateTeamScore(Team team)
    {
        return RacerScores.Where(rs => rs.Key.Team == team).Sum(rs => rs.Value);
    }

    public void SetLevelIndex(Level level)
    {
        ChatApi.SendMessage($"SetLevel");
        var levels = ZeepkistNetwork.CurrentLobby.Playlist;
        int i = 0;
        foreach (var zeeplevel in levels)
        {
            LevelIndex = i;
            ChatApi.SendMessage($"{i} {zeeplevel.WorkshopID} -> {level.WorkshopId}");
            if (zeeplevel.WorkshopID == level.WorkshopId)
            {
                ChatApi.SendMessage($"{i} {zeeplevel.WorkshopID} -> {level.WorkshopId}");
                
                break;
            }

            i++;
        }
    }
}