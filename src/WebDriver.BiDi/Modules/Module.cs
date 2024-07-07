using OpenQA.Selenium.BiDi.Communication;

namespace OpenQA.Selenium.BiDi.Modules;

internal abstract class Module(Broker broker)
{
    protected Broker Broker { get; } = broker;
}
