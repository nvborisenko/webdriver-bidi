using OpenQA.Selenium.BiDi.Internal;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class CreateCommand : Command<CreateCommandParameters>
{
    public override string Method { get; } = "browsingContext.create";
}

public class CreateCommandParameters : CommandParameters
{
    public BrowsingContextType Type { get; set; } = BrowsingContextType.Tab;

    public BrowsingContext? ReferenceContext { get; set; }
}

public enum BrowsingContextType
{
    Tab,
    Window
}

public class CreateResult
{
    [JsonInclude]
    public BrowsingContext Context { get; internal set; }
}