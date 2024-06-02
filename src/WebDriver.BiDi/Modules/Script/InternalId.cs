namespace OpenQA.Selenium.BiDi.Modules.Script;

public class InternalId
{
    readonly BiDi.Session _session;

    public InternalId(BiDi.Session session, string id)
    {
        _session = session;
        Id = id;
    }

    public string Id { get; }
}
