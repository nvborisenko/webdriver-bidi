using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class TraverseHistoryCommand(TraverseHistoryParameters parameters) : Command<TraverseHistoryParameters>("browsingContext.traverseHistory", parameters)
{

}

public class TraverseHistoryParameters : CommandParameters
{
    public BrowsingContext Context { get; set; }

    public int Delta { get; set; }
}

public class TraverseHistoryResult
{

}
