using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class TraverseHistoryCommand(TraverseHistoryCommandParameters @params) : Command<TraverseHistoryCommandParameters>(@params);

internal class TraverseHistoryCommandParameters(BrowsingContext context, int delta) : CommandParameters
{
    public BrowsingContext Context { get; } = context;

    public int Delta { get; } = delta;
}

public class TraverseHistoryResult
{

}
