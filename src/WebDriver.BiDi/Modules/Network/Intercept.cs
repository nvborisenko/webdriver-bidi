using System;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public class Intercept : IAsyncDisposable
{
    private readonly BiDi.Session _session;

    internal Intercept(BiDi.Session session, string id)
    {
        _session = session;
        Id = id;
    }

    public string Id { get; private set; }

    public Task RemoveAsync()
    {
        var @params = new RemoveInterceptCommand.Parameters(this);

        return _session.NetworkModule.RemoveInterceptAsync(@params);
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
