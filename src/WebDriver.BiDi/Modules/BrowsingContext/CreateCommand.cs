using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class CreateCommand : Command<CreateCommandParameters>
{
    public override string Method { get; } = "browsingContext.create";
}

internal class CreateCommandParameters : EmptyCommandParameters
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
    public string Context { get; set; } = null!;
}