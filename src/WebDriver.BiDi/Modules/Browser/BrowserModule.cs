using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Browser
{
    public class BrowserModule
    {
        private Broker _broker;

        public BrowserModule(Broker broker)
        {
            _broker = broker;
        }

        public async Task<EmptyResult> CloseAsync()
        {
            return await _broker.ExecuteCommand<CloseCommand, EmptyResult>(new CloseCommand());
        }
    }
}
