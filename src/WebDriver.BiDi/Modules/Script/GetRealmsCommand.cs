using OpenQA.Selenium.BiDi.Communication;
using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.Script;

internal class GetRealmsCommand(GetRealmsCommandParameters @params) : Command<GetRealmsCommandParameters>(@params);

internal class GetRealmsCommandParameters : CommandParameters
{
    public BrowsingContext.BrowsingContext? Context { get; set; }

    public RealmType? Type { get; set; }
}

public class RealmsOptions
{
    public BrowsingContext.BrowsingContext? Context { get; set; }

    public RealmType? Type { get; set; }
}

internal class GetRealmsResult(IReadOnlyList<RealmInfo> realms)
{
    public IReadOnlyList<RealmInfo> Realms { get; } = realms;
}
