﻿using OpenQA.Selenium.BiDi.Communication;
using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.Script;

internal class GetRealmsCommand(GetRealmsCommandParameters @params) : Command<GetRealmsCommandParameters>(@params);

internal class GetRealmsCommandParameters : CommandParameters
{
    public BrowsingContext.BrowsingContext? Context { get; set; }

    public RealmType? Type { get; set; }
}

internal class GetRealmsResult(IReadOnlyList<RealmInfoEventArgs> realms)
{
    public IReadOnlyList<RealmInfoEventArgs> Realms { get; } = realms;
}
