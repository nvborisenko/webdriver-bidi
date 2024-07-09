using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Script
{
    [JsonDerivedType(typeof(RealmTarget))]
    [JsonDerivedType(typeof(ContextTarget))]
    public abstract class Target
    {

    }

    public class RealmTarget : Target
    {

    }

    public class ContextTarget(BrowsingContext.BrowsingContext context) : Target
    {
        public BrowsingContext.BrowsingContext Context { get; } = context;
    }
}
