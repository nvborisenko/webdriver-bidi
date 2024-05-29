using OpenQA.Selenium.BiDi.Internal;
using OpenQA.Selenium.BiDi.Modules.Script;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class LocateNodesCommand(LocateNodesParameters parameters) : Command<LocateNodesParameters>("browsingContext.locateNodes", parameters)
{

}

public class LocateNodesParameters : CommandParameters
{
    public string Context { get; set; }

    public Locator Locator { get; set; }
}

public class LocateNodesResult
{
    [JsonInclude]
    public IReadOnlyList<NodeRemoteValue> Nodes { get; internal set; }
}