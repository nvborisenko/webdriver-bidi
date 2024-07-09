namespace OpenQA.Selenium.BiDi.Modules.Network;

public record Initiator(InitiatorType Type)
{
    public uint? ColumnNumber { get; set; }

    public uint? LineNumber { get; set; }

    public Script.StackTrace? StackTrace { get; set; }

    public Request? Request { get; set; }
}

public enum InitiatorType
{
    Parser,
    Script,
    Preflight,
    Other
}