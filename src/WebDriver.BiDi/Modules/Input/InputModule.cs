﻿using OpenQA.Selenium.BiDi.Internal;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Input;

internal sealed class InputModule
{
    private readonly Broker _broker;

    internal InputModule(Broker broker)
    {
        _broker = broker;
    }

    public async Task PerformActionsAsync(PerformActionsCommand.Parameters @params)
    {
        await _broker.ExecuteCommandAsync(new PerformActionsCommand(@params)).ConfigureAwait(false);
    }

    public async Task ReleaseActionsAsync(ReleaseActionsCommand.Parameters @params)
    {
        await _broker.ExecuteCommandAsync(new ReleaseActionsCommand(@params));
    }
}
