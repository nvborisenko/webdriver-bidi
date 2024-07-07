using OpenQA.Selenium.BiDi.Communication;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Script;

internal sealed class ScriptModule(Broker broker) : Module(broker)
{
    public async Task<EvaluateResultSuccess> EvaluateAsync(EvaluateCommandParameters @params)
    {
        var result = await Broker.ExecuteCommandAsync<EvaluateResult>(new EvaluateCommand(@params)).ConfigureAwait(false);

        if (result is EvaluateResultException exp)
        {
            throw new ScriptEvaluateException(exp);
        }

        return (EvaluateResultSuccess)result;
    }

    public async Task<EvaluateResultSuccess> CallFunctionAsync(CallFunctionCommandParameters @params)
    {
        var result = await Broker.ExecuteCommandAsync<EvaluateResult>(new CallFunctionCommand(@params)).ConfigureAwait(false);

        if (result is EvaluateResultException exp)
        {
            throw new ScriptEvaluateException(exp);
        }

        return (EvaluateResultSuccess)result;
    }

    public async Task<GetRealmsResult> GetRealmAsync(GetRealmsCommandParameters @params)
    {
        return await Broker.ExecuteCommandAsync<GetRealmsResult>(new GetRealmsCommand(@params)).ConfigureAwait(false);
    }

    public async Task<AddPreloadScriptResult> AddPreloadScriptAsync(AddPreloadScriptCommandParameters @params)
    {
        return await Broker.ExecuteCommandAsync<AddPreloadScriptResult>(new AddPreloadScriptCommand(@params)).ConfigureAwait(false);
    }

    public async Task RemovePreloadScriptAsync(RemovePreloadScriptCommandParameters @params)
    {
        await Broker.ExecuteCommandAsync(new RemovePreloadScriptCommand(@params)).ConfigureAwait(false);
    }
}
