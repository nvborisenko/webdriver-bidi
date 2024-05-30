using OpenQA.Selenium.BiDi.Internal;
using OpenQA.Selenium.BiDi.Modules.Script;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class LocateNodesCommand(LocateNodesCommand.Parameters @params)
    : Command<LocateNodesCommand.Parameters>("browsingContext.locateNodes", @params)
{
    internal class Parameters : CommandParameters
    {
        public string Context { get; set; }

        public Locator Locator { get; set; }
    }
}

public class LocateNodesResult
{
    [JsonInclude]
    public IReadOnlyList<NodeRemoteValue> Nodes { get; internal set; }
}
