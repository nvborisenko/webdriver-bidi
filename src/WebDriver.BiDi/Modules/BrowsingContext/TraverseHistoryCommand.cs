using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class TraverseHistoryCommand : Command<TraverseHistoryParameters>
{
    public override string Method => "browsingContext.traverseHistory";
}

public class TraverseHistoryParameters : CommandParameters
{
    public BrowsingContext Context { get; set; }

    public int Delta { get; set; }
}

public class TraverseHistoryResult
{

}
