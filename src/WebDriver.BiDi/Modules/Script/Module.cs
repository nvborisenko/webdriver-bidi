using OpenQA.Selenium.BiDi.Internal;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Script;

internal sealed class Module
{
    private readonly Broker _broker;

    internal Module(Broker broker)
    {
        _broker = broker;
    }

    public async Task<EvaluateResultSuccess> EvaluateAsync(EvaluateCommandParameters @params)
    {
        var result = await _broker.ExecuteCommandAsync<EvaluateResult>(new EvaluateCommand(@params)).ConfigureAwait(false);

        if (result is EvaluateResultException exp)
        {
            throw new ScriptEvaluateException(exp);
        }

        return (EvaluateResultSuccess)result;
    }

    public async Task<EvaluateResultSuccess> CallFunctionAsync(CallFunctionCommandParameters @params)
    {
        var result = await _broker.ExecuteCommandAsync<EvaluateResult>(new CallFunctionCommand(@params)).ConfigureAwait(false);

        if (result is EvaluateResultException exp)
        {
            throw new ScriptEvaluateException(exp);
        }

        return (EvaluateResultSuccess)result;
    }

    public async Task<GetRealmsResult> GetRealmAsync(GetRealmsCommandParameters @params)
    {
        return await _broker.ExecuteCommandAsync<GetRealmsResult>(new GetRealmsCommand(@params)).ConfigureAwait(false);
    }

    public async Task<AddPreloadScriptResult> AddPreloadScriptAsync(AddPreloadScriptCommandParameters @params)
    {
        return await _broker.ExecuteCommandAsync<AddPreloadScriptResult>(new AddPreloadScriptCommand(@params)).ConfigureAwait(false);
    }

    public async Task RemovePreloadScriptAsync(RemovePreloadScriptCommandParameters @params)
    {
        await _broker.ExecuteCommandAsync(new RemovePreloadScriptCommand(@params)).ConfigureAwait(false);
    }
}
