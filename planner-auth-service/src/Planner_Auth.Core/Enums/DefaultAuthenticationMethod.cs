using System.Text.Json.Serialization;

namespace Planner_Auth.Core.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DefaultAuthenticationMethod
    {
        Email,
        Phone
    }
}