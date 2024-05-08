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

    public class ContextTarget : Target
    {
        public string Context { get; set; }
    }
}
