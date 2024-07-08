﻿using OpenQA.Selenium.BiDi.Communication;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Input;

internal sealed class InputModule(Broker broker) : Module(broker)
{
    public async Task PerformActionsAsync(BrowsingContext.BrowsingContext context, PerformActionsOptions? options = default)
    {
        var @params = new PerformActionsCommandParameters(context);

        if (options is not null)
        {
            @params.Actions = options.Actions;
        }

        await Broker.ExecuteCommandAsync(new PerformActionsCommand(@params)).ConfigureAwait(false);
    }

    public async Task ReleaseActionsAsync(BrowsingContext.BrowsingContext context)
    {
        var @params = new ReleaseActionsCommandParameters(context);

        await Broker.ExecuteCommandAsync(new ReleaseActionsCommand(@params));
    }
}
