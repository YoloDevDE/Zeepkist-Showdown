namespace Showdown3.Models;

public class Level
{
    public Level(ulong workshopId, string levelName, string authorName)
    {
        WorkshopId = workshopId;
        LevelName = levelName;
        AuthorName = authorName;
    }

    public Level(LevelScriptableObject levelScriptableObject)
    {
        WorkshopId = levelScriptableObject.WorkshopID;
        LevelName = levelScriptableObject.Name;
        AuthorName = levelScriptableObject.Author;
    }

    public ulong WorkshopId { get; }
    public string LevelName { get; }
    public string AuthorName { get; }
}