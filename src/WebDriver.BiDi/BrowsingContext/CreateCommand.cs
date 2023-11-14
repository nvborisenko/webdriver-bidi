namespace OpenQA.Selenium.BiDi.BrowsingContext;

internal class CreateCommand : Command<CreateCommandParameters>
{
    public override string Method { get; } = "browsingContext.create";
}

internal class CreateCommandParameters
{
    public BrowsingContextType Type { get; } = BrowsingContextType.Tab;
}

internal enum BrowsingContextType
{
    Tab,
    Window
}
