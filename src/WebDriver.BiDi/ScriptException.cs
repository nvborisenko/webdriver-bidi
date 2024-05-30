using System;
using OpenQA.Selenium.BiDi.Modules.Script;

namespace OpenQA.Selenium.BiDi;

public class ScriptException(EvaluateResultException evaluateResultException) : Exception
{
    private readonly EvaluateResultException _evaluateResultException = evaluateResultException;

    public string Text => _evaluateResultException.ExceptionDetails.Text;

    public uint ColumNumber => _evaluateResultException.ExceptionDetails.ColumnNumber;

    public override string Message => $"{Text}{Environment.NewLine}{_evaluateResultException.ExceptionDetails.StackTrace}";
}
