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

    public async Task<EvaluateResultSuccess> EvaluateAsync(EvaluateCommand.Parameters @params)
    {
        var result = await _broker.ExecuteCommandAsync<EvaluateResult>(new EvaluateCommand(@params)).ConfigureAwait(false);

        if (result is EvaluateResultException exp)
        {
            throw new ScriptException(exp);
        }

        return (EvaluateResultSuccess)result;
    }

    public async Task<EvaluateResultSuccess> CallFunctionAsync(CallFunctionCommand.Parameters @params)
    {
        var result = await _broker.ExecuteCommandAsync<EvaluateResult>(new CallFunctionCommand(@params)).ConfigureAwait(false);

        if (result is EvaluateResultException exp)
        {
            throw new ScriptException(exp);
        }

        return (EvaluateResultSuccess)result;
    }
}
