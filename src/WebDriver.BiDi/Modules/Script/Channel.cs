namespace OpenQA.Selenium.BiDi.Modules.Script;

public class Channel
{
    readonly BiDi.Session _session;

    internal Channel(BiDi.Session session, string id)
    {
        _session = session;
        Id = id;
    }

    internal string Id { get; }
}
