namespace OpenQA.Selenium.BiDi.Modules.Script;

public record StackFrame(uint LineNumber, uint ColumnNumber, string Url, string FunctionName);
