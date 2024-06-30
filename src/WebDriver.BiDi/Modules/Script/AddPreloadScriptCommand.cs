using OpenQA.Selenium.BiDi.Internal;
using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.Script;

internal class AddPreloadScriptCommand(AddPreloadScriptCommandParameters @params) : Command<AddPreloadScriptCommandParameters>(@params);

internal class AddPreloadScriptCommandParameters(string functionDeclaration) : CommandParameters
{
    public string FunctionDeclaration { get; } = functionDeclaration;

    public IEnumerable<ChannelValue>? Arguments { get; set; }

    public IEnumerable<BrowsingContext.BrowsingContext>? Contexts { get; set; }

    public string? Sandbox { get; set; }
}

internal class AddPreloadScriptResult(PreloadScript script)
{
    public PreloadScript Script { get; } = script;
}
