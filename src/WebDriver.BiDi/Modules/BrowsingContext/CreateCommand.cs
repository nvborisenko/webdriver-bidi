using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class CreateCommand(CreateCommand.Parameters @params)
    : Command<CreateCommand.Parameters>("browsingContext.create", @params)
{
    internal class Parameters(BrowsingContextType type) : CommandParameters
    {
        public BrowsingContextType Type { get; } = type;

        public BrowsingContext? ReferenceContext { get; set; }

        public bool? Background { get; set; }

        public Browser.UserContext? UserContext { get; set; }
    }
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
