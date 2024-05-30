namespace OpenQA.Selenium.BiDi.Modules.Script;

public class StackFrame(uint lineNumber, uint columnNumber, string url, string functionName)
{
    public uint LineNumber { get; } = lineNumber;

    public uint ColumnNumber { get; } = columnNumber;

    public string Url { get; } = url;

    public string FunctionName { get; } = functionName;

    public override string ToString()
    {
        return $"{LineNumber}:{ColumnNumber} {Url} - {FunctionName}";
    }
}
