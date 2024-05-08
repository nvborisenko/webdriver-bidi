using OpenQA.Selenium.BiDi.Internal;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Session;

public sealed class SessionModule
{
    private readonly Broker _broker;

    internal SessionModule(Broker broker)
    {
        _broker = broker;
    }

    public async Task<StatusResult> StatusAsync()
    {
        return await _broker.ExecuteCommandAsync<StatusCommand, StatusResult>(new StatusCommand()).ConfigureAwait(false);
    }
}
