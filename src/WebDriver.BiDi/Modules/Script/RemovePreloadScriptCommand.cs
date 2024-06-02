using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Script;

internal class RemovePreloadScriptCommand(RemovePreloadScriptCommand.Parameters @params)
    : Command<RemovePreloadScriptCommand.Parameters>("script.removePreloadScript", @params)
{
    internal class Parameters(PreloadScript script) : CommandParameters
    {
        public PreloadScript Script { get; } = script;
    }
}
