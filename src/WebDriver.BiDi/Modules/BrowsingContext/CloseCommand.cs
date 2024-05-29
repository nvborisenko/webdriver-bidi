using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class CloseCommand(CloseCommandParameters parameters) : Command<CloseCommandParameters>("browsingContext.close", parameters)
{

}

public class CloseCommandParameters : CommandParameters
{
    public BrowsingContext Context { get; set; }
}
