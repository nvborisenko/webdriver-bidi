namespace OpenQA.Selenium.BiDi.Internal;

internal abstract class Command
{
    public int? Id { get; set; }
}

internal abstract class Command<TCommandParameters> : Command
    where TCommandParameters : EmptyCommandParameters, new()
{
    public abstract string Method { get; }

    public TCommandParameters Params { get; set; } = new TCommandParameters();
}
