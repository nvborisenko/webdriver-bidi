namespace OpenQA.Selenium.BiDi.Modules.Script;

public class Handle
{
    readonly BiDi.Session _session;

    public Handle(BiDi.Session session, string id)
    {
        _session = session;
        Id = id;
    }

    public string Id { get; }
}
