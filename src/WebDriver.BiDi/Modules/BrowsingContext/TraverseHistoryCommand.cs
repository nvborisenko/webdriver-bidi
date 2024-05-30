using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class TraverseHistoryCommand(TraverseHistoryCommand.Parameters @params)
    : Command<TraverseHistoryCommand.Parameters>("browsingContext.traverseHistory", @params)
{
    internal class Parameters : CommandParameters
    {
        public BrowsingContext Context { get; set; }

        public int Delta { get; set; }
    }
}

public class TraverseHistoryResult
{

}
