using OpenQA.Selenium.BiDi.Communication;
using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class GetTreeCommand(GetTreeCommandParameters @params) : Command<GetTreeCommandParameters>(@params);

internal class GetTreeCommandParameters : CommandParameters
{
    public uint? MaxDepth { get; set; }

    public BrowsingContext? Root { get; set; }
}

public class GetTreeResult(IReadOnlyList<BrowsingContextInfo> contexts)
{
    public IReadOnlyList<BrowsingContextInfo> Contexts { get; } = contexts;
}
