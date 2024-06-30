using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Session;

internal class StatusCommand() : Command<CommandParameters>(CommandParameters.Empty);

public class StatusResult(bool ready, string message)
{
    public bool Ready { get; } = ready;

    public string Message { get; } = message;
}
