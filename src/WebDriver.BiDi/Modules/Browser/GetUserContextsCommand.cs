using OpenQA.Selenium.BiDi.Internal;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Browser;

internal class GetUserContextsCommand()
    : Command<CommandParameters>("browser.getUserContexts", CommandParameters.Empty)
{
}

public class GetUserContextsResult
{
    [JsonInclude]
    public IReadOnlyList<UserContextInfo> UserContexts { get; internal set; }
}