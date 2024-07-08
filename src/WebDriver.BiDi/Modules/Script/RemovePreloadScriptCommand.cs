using OpenQA.Selenium.BiDi.Communication;

namespace OpenQA.Selenium.BiDi.Modules.Script;

internal class RemovePreloadScriptCommand(RemovePreloadScriptCommandParameters @params) : Command<RemovePreloadScriptCommandParameters>(@params);

internal class RemovePreloadScriptCommandParameters(PreloadScript script) : CommandParameters
{
    public PreloadScript Script { get; } = script;
}

public class RemovePreloadScriptOptions : CommandOptions;
