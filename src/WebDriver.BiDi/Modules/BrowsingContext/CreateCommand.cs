using OpenQA.Selenium.BiDi.Internal;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class CreateCommand : Command<CreateCommandParameters>
{
    public override string Method { get; } = "browsingContext.create";
}

internal class CreateCommandParameters : CommandParameters
{
    public BrowsingContextType Type { get; } = BrowsingContextType.Tab;
}

internal enum BrowsingContextType
{
    Tab,
    Window
}

public class CreateResult
{
    [JsonInclude]
    public BrowsingContext Context { get; internal set; }
}