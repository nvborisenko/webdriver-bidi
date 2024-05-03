using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Input;

internal class ReleaseActionsCommand : Command<ReleaseActionsParameters>
{
    public override string Method => "input.releaseActions";
}

public class ReleaseActionsParameters : EmptyCommandParameters
{
    public string Context { get; set; }
}
