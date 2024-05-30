using OpenQA.Selenium.BiDi.Internal;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class GetTreeCommand(GetTreeCommand.Parameters @params)
    : Command<GetTreeCommand.Parameters>("browsingContext.getTree", @params)
{
    internal class Parameters : CommandParameters
    {
        public uint? MaxDepth { get; set; }

        public BrowsingContext Root { get; set; }
    }
}

public class GetTreeResult
{
    [JsonInclude]
    public IReadOnlyList<BrowsingContextInfo> Contexts { get; internal set; }
}