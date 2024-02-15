using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Browser;

public sealed class BrowserModule
{
    private readonly Broker _broker;

    internal BrowserModule(Broker broker)
    {
        _broker = broker;
    }

    public async Task<EmptyResult> CloseAsync()
    {
        return await _broker.ExecuteCommandAsync<CloseCommand, EmptyResult>(new CloseCommand());
    }
}
