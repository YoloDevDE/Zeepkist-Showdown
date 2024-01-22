using System.Collections.Generic;

namespace Showdown3.Models;

public class ShowdownLevel
{
    public ShowdownLevel(ulong workshopId, string levelName)
    {
        WorkshopId = workshopId;
        LevelName = levelName;
    }

    public ShowdownLevel(LevelScriptableObject levelScriptableObject)
    {
        WorkshopId = levelScriptableObject.WorkshopID;
        LevelName = levelScriptableObject.Name;
    }

    public ulong WorkshopId { get; }
    public string LevelName { get; }
}