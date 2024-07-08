using OpenQA.Selenium.BiDi.Communication;
using System;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Script;

internal sealed class ScriptModule(Broker broker) : Module(broker)
{
    public async Task<EvaluateResultSuccess> EvaluateAsync(EvaluateCommandParameters @params, EvaluateOptions? options = default)
    {
        if (options is not null)
        {
            @params.ResultOwnership = options.ResultOwnership;
            @params.SerializationOptions = options.SerializationOptions;
            @params.UserActivation = options.UserActivation;
        }

        var result = await Broker.ExecuteCommandAsync<EvaluateResult>(new EvaluateCommand(@params)).ConfigureAwait(false);

        if (result is EvaluateResultException exp)
        {
            throw new ScriptEvaluateException(exp);
        }

        return (EvaluateResultSuccess)result;
    }

    public async Task<EvaluateResultSuccess> CallFunctionAsync(CallFunctionCommandParameters @params, CallFunctionOptions? options = default)
    {
        if (options is not null)
        {
            @params.Arguments = options.Arguments;
            @params.ResultOwnership = options.ResultOwnership;
            @params.SerializationOptions = options.SerializationOptions;
            @params.This = options.This;
            @params.UserActivation = options.UserActivation;
        }

        var result = await Broker.ExecuteCommandAsync<EvaluateResult>(new CallFunctionCommand(@params)).ConfigureAwait(false);

        if (result is EvaluateResultException exp)
        {
            throw new ScriptEvaluateException(exp);
        }

        return (EvaluateResultSuccess)result;
    }

    public async Task<GetRealmsResult> GetRealmAsync(GetRealmsCommandParameters @params, RealmsOptions? options = default)
    {
        if (options is not null)
        {
            @params.Context = options.Context;
            @params.Type = options.Type;
        }

        return await Broker.ExecuteCommandAsync<GetRealmsResult>(new GetRealmsCommand(@params)).ConfigureAwait(false);
    }

    public async Task<AddPreloadScriptResult> AddPreloadScriptAsync(AddPreloadScriptCommandParameters @params, PreloadScriptOptions? options = default)
    {
        if (options is not null)
        {
            @params.Contexts = options.Contexts;
            @params.Arguments = options.Arguments;
            @params.Sandbox = options.Sandbox;
        }

        return await Broker.ExecuteCommandAsync<AddPreloadScriptResult>(new AddPreloadScriptCommand(@params)).ConfigureAwait(false);
    }

    public async Task RemovePreloadScriptAsync(RemovePreloadScriptCommandParameters @params)
    {
        await Broker.ExecuteCommandAsync(new RemovePreloadScriptCommand(@params)).ConfigureAwait(false);
    }
}
