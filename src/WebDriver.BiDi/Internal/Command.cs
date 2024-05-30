namespace OpenQA.Selenium.BiDi.Internal;

internal abstract class Command(string method)
{
    public int Id { get; internal set; }

    public string Method { get; } = method;
}

internal abstract class Command<TCommandParameters>(string method, TCommandParameters @params)
    : Command(method)
    where TCommandParameters : CommandParameters
{
    public TCommandParameters Params { get; } = @params;
}
