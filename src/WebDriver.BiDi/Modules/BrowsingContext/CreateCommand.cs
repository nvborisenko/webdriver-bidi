using OpenQA.Selenium.BiDi.Internal;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class CreateCommand(CreateCommandParameters parameters)
    : Command<CreateCommandParameters>("browsingContext.create", parameters)
{

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