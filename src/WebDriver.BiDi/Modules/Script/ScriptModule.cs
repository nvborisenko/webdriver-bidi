using OpenQA.Selenium.BiDi.Internal;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Script;

internal sealed class ScriptModule
{
    private readonly Broker _broker;

    internal ScriptModule(Broker broker)
    {
        _broker = broker;
    }

    public async Task<EvaluateResult> EvaluateAsync(EvaluateCommand.Parameters @params)
    {
        return await _broker.ExecuteCommandAsync<EvaluateResult>(new EvaluateCommand(@params)).ConfigureAwait(false);
    }
}
