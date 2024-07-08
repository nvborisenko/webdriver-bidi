using OpenQA.Selenium.BiDi.Communication;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class ActivateCommand(ActivateCommandParameters @params) : Command<ActivateCommandParameters>(@params);

internal class ActivateCommandParameters(BrowsingContext context) : CommandParameters
{
    public BrowsingContext Context { get; } = context;
}

public class ActivateOptions : CommandOptions;
