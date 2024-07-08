using OpenQA.Selenium.BiDi.Communication;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Script;

internal sealed class ScriptModule(Broker broker) : Module(broker)
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

        var result = await Broker.ExecuteCommandAsync<EvaluateResult>(new EvaluateCommand(@params)).ConfigureAwait(false);

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

        var result = await Broker.ExecuteCommandAsync<EvaluateResult>(new CallFunctionCommand(@params)).ConfigureAwait(false);

        if (result is EvaluateResultException exp)
        {
            throw new ScriptEvaluateException(exp);
        }

        return (EvaluateResultSuccess)result;
    }

    public async Task<GetRealmsResult> GetRealmAsync(RealmsOptions? options = default)
    {
        var @params = new GetRealmsCommandParameters();

        if (options is not null)
        {
            @params.Context = options.Context;
            @params.Type = options.Type;
        }

        return await Broker.ExecuteCommandAsync<GetRealmsResult>(new GetRealmsCommand(@params)).ConfigureAwait(false);
    }

    public async Task<AddPreloadScriptResult> AddPreloadScriptAsync(string functionDeclaration, PreloadScriptOptions? options = default)
    {
        var @params = new AddPreloadScriptCommandParameters(functionDeclaration);

        if (options is not null)
        {
            @params.Contexts = options.Contexts;
            @params.Arguments = options.Arguments;
            @params.Sandbox = options.Sandbox;
        }

        return await Broker.ExecuteCommandAsync<AddPreloadScriptResult>(new AddPreloadScriptCommand(@params)).ConfigureAwait(false);
    }

    public async Task RemovePreloadScriptAsync(PreloadScript script)
    {
        var @params = new RemovePreloadScriptCommandParameters(script);

        await Broker.ExecuteCommandAsync(new RemovePreloadScriptCommand(@params)).ConfigureAwait(false);
    }
}
