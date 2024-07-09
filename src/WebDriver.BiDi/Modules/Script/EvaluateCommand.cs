using OpenQA.Selenium.BiDi.Communication;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Script;

internal class EvaluateCommand(EvaluateCommandParameters @params) : Command<EvaluateCommandParameters>(@params);

internal class EvaluateCommandParameters(string expression, Target target, bool awaitPromise) : CommandParameters
{
    public string Expression { get; } = expression;

    public Target Target { get; } = target;

    public bool AwaitPromise { get; set; } = awaitPromise;

    public ResultOwnership? ResultOwnership { get; set; }

    public SerializationOptions? SerializationOptions { get; set; }

    public bool? UserActivation { get; set; }
}

public class EvaluateOptions : CommandOptions
{
    public ResultOwnership? ResultOwnership { get; set; }

    public SerializationOptions? SerializationOptions { get; set; }

    public bool? UserActivation { get; set; }
}

// https://github.com/dotnet/runtime/issues/72604
//[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
//[JsonDerivedType(typeof(EvaluateResultSuccess), "success")]
//[JsonDerivedType(typeof(EvaluateResultException), "exception")]
public abstract class EvaluateResult
{

}

public class EvaluateResultSuccess(RemoteValue result) : EvaluateResult
{
    public RemoteValue Result { get; } = result;

    public static implicit operator int(EvaluateResultSuccess r) => (int)(r.Result as NumberRemoteValue).Value;
    public static implicit operator long(EvaluateResultSuccess r) => (r.Result as NumberRemoteValue).Value;
    public static implicit operator NodeRemoteValue(EvaluateResultSuccess r) => r.Result as NodeRemoteValue;
    public static implicit operator string(EvaluateResultSuccess r)
    {
        return r.Result switch
        {
            StringRemoteValue stringValue => stringValue.Value,
            NullRemoteValue => null,
            _ => throw new System.Exception($"Cannot convert {r.Result} to string")
        };
    }
}

public class EvaluateResultException(ExceptionDetails exceptionDetails) : EvaluateResult
{
    public ExceptionDetails ExceptionDetails { get; } = exceptionDetails;
}

public class ExceptionDetails(uint columnNumber, uint lineNumber, StackTrace stackTrace, string text)
{
    public uint ColumnNumber { get; } = columnNumber;

    public uint LineNumber { get; } = lineNumber;

    public StackTrace StackTrace { get; } = stackTrace;

    public string Text { get; } = text;
}
