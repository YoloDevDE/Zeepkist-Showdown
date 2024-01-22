﻿using System.Collections.Generic;
using Showdown3.Models;

namespace Showdown3;

public class Match
{
    private List<Race> _races;
    private int count = 0;
    public Match(Team teamA, Team teamB)
    {
        TeamA = teamA;
        TeamB = teamB;
        _races = new List<Race>();
    }

    public Team TeamA { get; }
    public Team TeamB { get; }

    public bool CheckIfWinContitionIsMet()
    {
        count++;
        return count > 3;
    }
}