using System.Threading.Tasks;
using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Browser;

public sealed class BrowserModule
{
    private readonly Broker _broker;

    internal BrowserModule(Broker broker)
    {
        _broker = broker;
    }

    public async Task CloseAsync()
    {
        await _broker.ExecuteCommandAsync(new CloseCommand()).ConfigureAwait(false);
    }
}
