using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Session;

internal class EndCommand : Command<EmptyCommandParameters>
{
    public override string Method { get; } = "session.end";
}
