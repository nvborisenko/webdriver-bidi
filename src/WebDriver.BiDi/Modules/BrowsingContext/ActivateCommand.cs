using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class ActivateCommand(ActivateParameters parameters) : Command<ActivateParameters>("browsingContext.activate", parameters)
{

}

public class ActivateParameters : CommandParameters
{
    public BrowsingContext Context { get; set; }
}
