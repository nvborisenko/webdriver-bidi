using OpenQA.Selenium.BiDi.Internal;
using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.Script;

internal class GetRealmsCommand(GetRealmsCommand.Parameters @params)
    : Command<GetRealmsCommand.Parameters>("script.getRealms", @params)
{
    internal class Parameters : CommandParameters
    {
        public BrowsingContext.BrowsingContext? Context { get; set; }

        public RealmType? Type { get; set; }
    }
}

internal class GetRealmsResult(IReadOnlyList<RealmInfoEventArgs> realms)
{
    public IReadOnlyList<RealmInfoEventArgs> Realms { get; } = realms;
}