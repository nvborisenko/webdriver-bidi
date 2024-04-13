namespace OpenQA.Selenium.BiDi.Internal;

internal abstract class Command
{
    public int? Id { get; set; }
}

internal abstract class Command<TParameters> : Command
    where TParameters : EmptyCommandParameters, new()
{
    public abstract string Method { get; }

    public TParameters Params { get; set; } = new TParameters();
}
