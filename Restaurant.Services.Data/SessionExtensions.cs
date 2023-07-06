using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Extensions;
public static class SessionExtensions
{
    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        var jsonString = session.GetString(key);
        return jsonString != null ? JsonSerializer.Deserialize<T>(jsonString) : default;
    }

    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        var jsonString = JsonSerializer.Serialize(value);
        session.SetString(key, jsonString);
    }
}
