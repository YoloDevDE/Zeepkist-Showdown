namespace Showdown3.Models;

public class Level
{
    public Level(ulong workshopId, string levelName)
    {
        WorkshopId = workshopId;
        LevelName = levelName;
    }

    public Level(LevelScriptableObject levelScriptableObject)
    {
        WorkshopId = levelScriptableObject.WorkshopID;
        LevelName = levelScriptableObject.Name;
    }

    public ulong WorkshopId { get; }
    public string LevelName { get; }
}