using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Script;

[JsonDerivedType(typeof(RealmTarget))]
[JsonDerivedType(typeof(ContextTarget))]
public abstract record Target;

public record RealmTarget : Target;

public record ContextTarget(BrowsingContext.BrowsingContext Context) : Target;
