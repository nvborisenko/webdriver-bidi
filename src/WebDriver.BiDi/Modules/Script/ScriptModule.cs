﻿using OpenQA.Selenium.BiDi.Communication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Script;

public sealed class ScriptModule(Broker broker) : Module(broker)
{
    public async Task<EvaluateResultSuccess> EvaluateAsync(string expression, bool awaitPromise, Target target, EvaluateOptions? options = default)
    {
        var @params = new EvaluateCommandParameters(expression, target, awaitPromise);

        if (options is not null)
        {
            @params.ResultOwnership = options.ResultOwnership;
            @params.SerializationOptions = options.SerializationOptions;
            @params.UserActivation = options.UserActivation;
        }

        var result = await Broker.ExecuteCommandAsync<EvaluateResult>(new EvaluateCommand(@params), options).ConfigureAwait(false);

        if (result is EvaluateResultException exp)
        {
            throw new ScriptEvaluateException(exp);
        }

        return (EvaluateResultSuccess)result;
    }

    public async Task<EvaluateResultSuccess> CallFunctionAsync(string functionDeclaration, bool awaitPromise, Target target, CallFunctionOptions? options = default)
    {
        var @params = new CallFunctionCommandParameters(functionDeclaration, awaitPromise, target);

        if (options is not null)
        {
            @params.Arguments = options.Arguments;
            @params.ResultOwnership = options.ResultOwnership;
            @params.SerializationOptions = options.SerializationOptions;
            @params.This = options.This;
            @params.UserActivation = options.UserActivation;
        }

        var result = await Broker.ExecuteCommandAsync<EvaluateResult>(new CallFunctionCommand(@params), options).ConfigureAwait(false);

        if (result is EvaluateResultException exp)
        {
            throw new ScriptEvaluateException(exp);
        }

        return (EvaluateResultSuccess)result;
    }

    public async Task<IReadOnlyList<RealmInfo>> GetRealmsAsync(GetRealmsOptions? options = default)
    {
        var @params = new GetRealmsCommandParameters();

        if (options is not null)
        {
            @params.Context = options.Context;
            @params.Type = options.Type;
        }

        var result = await Broker.ExecuteCommandAsync<GetRealmsResult>(new GetRealmsCommand(@params), options).ConfigureAwait(false);

        return result.Realms;
    }

    public async Task<PreloadScript> AddPreloadScriptAsync(string functionDeclaration, PreloadScriptOptions? options = default)
    {
        var @params = new AddPreloadScriptCommandParameters(functionDeclaration);

        if (options is not null)
        {
            @params.Contexts = options.Contexts;
            @params.Arguments = options.Arguments;
            @params.Sandbox = options.Sandbox;
        }

        var result = await Broker.ExecuteCommandAsync<AddPreloadScriptResult>(new AddPreloadScriptCommand(@params), options).ConfigureAwait(false);

        return result.Script;
    }

    public async Task RemovePreloadScriptAsync(PreloadScript script, RemovePreloadScriptOptions? options = default)
    {
        var @params = new RemovePreloadScriptCommandParameters(script);

        await Broker.ExecuteCommandAsync(new RemovePreloadScriptCommand(@params), options).ConfigureAwait(false);
    }
}
