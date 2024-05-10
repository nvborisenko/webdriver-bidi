﻿using OpenQA.Selenium.BiDi.Internal;
using System;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Script;

public sealed class ScriptModule
{
    private readonly Broker _broker;

    internal ScriptModule(Broker broker)
    {
        _broker = broker;
    }

    public async Task<EvaluateResult> EvaluateAsync(EvaluateCommandParameters parameters)
    {
        return await _broker.ExecuteCommandAsync<EvaluateCommand, EvaluateResult>(new EvaluateCommand { Params = parameters }).ConfigureAwait(false);
    }
}