using OpenQA.Selenium.BiDi.Communication;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class CreateCommand(CreateCommandParameters @params) : Command<CreateCommandParameters>(@params);

internal class CreateCommandParameters(BrowsingContextType type) : CommandParameters
{
    public BrowsingContextType Type { get; } = type;

    public BrowsingContext? ReferenceContext { get; set; }

    public bool? Background { get; set; }

    public Browser.UserContext? UserContext { get; set; }
}

public class BrowsingContextOptions
{
    public BrowsingContext? ReferenceContext { get; set; }

    public bool? Background { get; set; }

    public Browser.UserContext? UserContext { get; set; }
}

public enum BrowsingContextType
{
    Tab,
    Window
}

public class CreateResult(BrowsingContext context)
{
    public BrowsingContext Context { get; } = context;
}
