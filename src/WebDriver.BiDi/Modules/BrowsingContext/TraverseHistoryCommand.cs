using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class TraverseHistoryCommand(TraverseHistoryCommand.Parameters @params)
    : Command<TraverseHistoryCommand.Parameters>("browsingContext.traverseHistory", @params)
{
    internal class Parameters(BrowsingContext context, int delta) : CommandParameters
    {
        public BrowsingContext Context { get; } = context;

        public int Delta { get; } = delta;
    }
}

public class TraverseHistoryResult
{

}
