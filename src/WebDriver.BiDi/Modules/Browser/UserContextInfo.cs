using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Browser;

public class UserContextInfo
{
    [JsonInclude]
    public UserContext UserContext { get; internal set; }
}
