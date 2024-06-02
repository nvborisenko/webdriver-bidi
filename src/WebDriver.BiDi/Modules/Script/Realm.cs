namespace OpenQA.Selenium.BiDi.Modules.Script;

public class Realm
{
    readonly BiDi.Session _session;

    public Realm(BiDi.Session session, string id)
    {
        _session = session;
        Id = id;
    }

    public string Id { get; }
}
