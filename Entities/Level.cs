using System.Collections.Generic;
using System.Threading.Tasks;
using Showdown3.Helper;

namespace Showdown3.Entities;

public class Level
{
    // Parameterloser Konstruktor für die Deserialisierung
    public Level()
    {
    }

    // Bestehende Konstruktoren...
    public Level(ulong workshopId, string name, string author)
    {
        WorkshopId = workshopId;
        Name = name;
        Author = author;
    }

    public Level(LevelScriptableObject levelScriptableObject)
    {
        WorkshopId = levelScriptableObject.WorkshopID;
        Name = levelScriptableObject.Name;
        Author = levelScriptableObject.Author;
    }

    // Eigenschaften
    public ulong WorkshopId { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }

    // Überschriebene ToString Methode
    public override string ToString()
    {
        return $"{Name}";
    }

    // Methode zum Abrufen der Levels vom Server
    public static async Task<HashSet<Level>> GetMatchFromServerAsync()
    {
        var jsonResponse = await new HttpHelper().GetAsync("/api/levels");
        return JsonHelper.DeserializeObject<HashSet<Level>>(jsonResponse);
    }
}