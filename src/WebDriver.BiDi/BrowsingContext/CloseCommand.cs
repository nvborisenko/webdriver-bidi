namespace OpenQA.Selenium.BiDi.BrowsingContext;

internal class CloseCommand : Command<CloseCommandParameters>
{
    public override string Name { get; } = "browsingContext.close";
}

internal class CloseCommandParameters
{
    public string? Context { get; set; }
}
