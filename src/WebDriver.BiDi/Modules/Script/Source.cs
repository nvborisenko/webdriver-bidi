namespace OpenQA.Selenium.BiDi.Modules.Script;

public class Source(Realm realm)
{
    public Realm Realm { get; } = realm;

    public BrowsingContext.BrowsingContext? Context { get; set; }
}
