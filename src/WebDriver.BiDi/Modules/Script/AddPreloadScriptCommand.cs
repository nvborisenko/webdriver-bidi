using OpenQA.Selenium.BiDi.Internal;
using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.Script;

internal class AddPreloadScriptCommand(AddPreloadScriptCommand.Parameters @params)
    : Command<AddPreloadScriptCommand.Parameters>("script.addPreloadScript", @params)
{
    internal class Parameters(string functionDeclaration) : CommandParameters
    {
        public string FunctionDeclaration { get; } = functionDeclaration;

        public IEnumerable<ChannelValue>? Arguments { get; set; }

        public IEnumerable<BrowsingContext.BrowsingContext>? Contexts { get; set; }

        public string? Sandbox { get; set; }
    }
}

internal class AddPreloadScriptResult(PreloadScript script)
{
    public PreloadScript Script { get; } = script;
}