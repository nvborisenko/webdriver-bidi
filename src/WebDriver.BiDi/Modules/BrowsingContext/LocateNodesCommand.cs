using OpenQA.Selenium.BiDi.Communication;
using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class LocateNodesCommand(LocateNodesCommandParameters @params) : Command<LocateNodesCommandParameters>(@params);

internal class LocateNodesCommandParameters(BrowsingContext context, Locator locator) : CommandParameters
{
    public BrowsingContext Context { get; } = context;

    public Locator Locator { get; } = locator;

    public uint? MaxNodeCount { get; set; }

    public Script.SerializationOptions? SerializationOptions { get; set; }

    public IEnumerable<Script.SharedReference>? StartNodes { get; set; }
}

public class NodesOptions : CommandOptions
{
    public uint? MaxNodeCount { get; set; }

    public Script.SerializationOptions? SerializationOptions { get; set; }

    public IEnumerable<Script.SharedReference>? StartNodes { get; set; }
}

public class LocateNodesResult(IReadOnlyList<Script.NodeRemoteValue> nodes)
{
    public IReadOnlyList<Script.NodeRemoteValue> Nodes { get; } = nodes;
}
