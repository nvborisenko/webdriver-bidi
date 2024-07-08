using OpenQA.Selenium.BiDi.Communication;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

class HandleUserPromptCommand(HandleUserPromptCommandParameters @params) : Command<HandleUserPromptCommandParameters>(@params);

internal class HandleUserPromptCommandParameters(BrowsingContext context) : CommandParameters
{
    public BrowsingContext Context { get; } = context;

    public bool? Accept { get; set; }

    public string? UserText { get; set; }
}

public class UserPromptOptions : CommandOptions
{
    public bool? Accept { get; set; }

    public string? UserText { get; set; }
}