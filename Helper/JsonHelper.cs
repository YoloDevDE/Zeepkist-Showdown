using Newtonsoft.Json;

namespace Showdown3.Helper;

public static class JsonHelper
{
    public static string SerializeObject(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    public static T DeserializeObject<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
}