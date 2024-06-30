using OpenQA.Selenium.BiDi.Internal;
using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.Browser;

internal class GetUserContextsCommand() : Command<CommandParameters>(CommandParameters.Empty);

public class GetUserContextsResult(IReadOnlyList<UserContextInfo> userContexts)
{
    public IReadOnlyList<UserContextInfo> UserContexts { get; } = userContexts;
}
