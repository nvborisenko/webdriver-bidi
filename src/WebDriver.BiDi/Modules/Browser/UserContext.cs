using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Browser;

public class UserContext
{
    private readonly BiDi.Session _session;

    internal UserContext(BiDi.Session session, string id)
    {
        _session = session;
        Id = id;
    }

    [JsonInclude]
    public string Id { get; internal set; }

    public async Task RemoveAsync()
    {
        var @params = new RemoveUserContextCommand.Parameters(this);

        await _session.Browser.RemoveUserContextAsync(@params).ConfigureAwait(false);
    }
}
