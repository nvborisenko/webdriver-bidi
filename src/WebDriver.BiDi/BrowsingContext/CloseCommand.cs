namespace OpenQA.Selenium.BiDi.BrowsingContext;

internal class CloseCommand : Command<CloseCommandParameters>
{
    public override string Method { get; } = "browsingContext.close";
}

internal class CloseCommandParameters
{
    public string? Context { get; set; }
}
