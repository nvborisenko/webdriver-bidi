using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Input;

internal class ReleaseActionsCommand(ReleaseActionsParameters parameters) : Command<ReleaseActionsParameters>("input.releaseActions", parameters)
{

}

public class ReleaseActionsParameters : CommandParameters
{
    public string Context { get; set; }
}
