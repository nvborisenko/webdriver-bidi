namespace OpenQA.Selenium.BiDi.Modules.Script;

public record StackFrame(ulong LineNumber, ulong ColumnNumber, string Url, string FunctionName);
