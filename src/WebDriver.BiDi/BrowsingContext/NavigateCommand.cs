namespace OpenQA.Selenium.BiDi.BrowsingContext;

internal class NavigateCommand : Command<NavigateCommandParameters>
{
    public override string Method { get; } = "browsingContext.navigate";
}

public class NavigateCommandParameters
{
    public string? Context { get; set; }

    public string? Url { get; set; }

    public ReadinessState? Wait { get; set; }
}

public enum ReadinessState
{
    None,
    Interactive,
    Complete
}

public class NavigateResult
{
    public string Navigation { get; set; }

    public string Url { get; set; }

}