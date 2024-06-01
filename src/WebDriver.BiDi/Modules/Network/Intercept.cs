using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public class Intercept
#if NET8_0_OR_GREATER
        : IAsyncDisposable
#endif
{
    private BiDi.Session _session;

    internal Intercept(BiDi.Session session, string id)
    {
        _session = session;
        Id = id;
    }

    public string Id { get; private set; }

    public Task RemoveAsync()
    {
        var @params = new RemoveInterceptCommand.Parameters(this);

        return _session.Network.RemoveInterceptAsync(@params);
    }

    public async ValueTask DisposeAsync()
    {
        await RemoveAsync();
    }

    public override bool Equals(object obj)
    {
        return Id == (obj as Intercept).Id;
    }


}
