using OpenQA.Selenium.BiDi.Communication;
using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class GetTreeCommand(GetTreeCommandParameters @params) : Command<GetTreeCommandParameters>(@params);

internal record GetTreeCommandParameters : CommandParameters
{
    public uint? MaxDepth { get; set; }

    public BrowsingContext? Root { get; set; }
}

public record TreeOptions : CommandOptions
{
    public uint? MaxDepth { get; set; }

    public BrowsingContext? Root { get; set; }
}

public record GetTreeResult(IReadOnlyList<BrowsingContextInfo> Contexts);
