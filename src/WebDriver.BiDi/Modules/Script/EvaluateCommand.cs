using OpenQA.Selenium.BiDi.Communication;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Script;

internal class EvaluateCommand(EvaluateCommandParameters @params) : Command<EvaluateCommandParameters>(@params);

internal class EvaluateCommandParameters(string expression, Target target, bool awaitPromise) : CommandParameters
{
    public string Expression { get; set; } = expression;

    public Target Target { get; set; } = target;

    public bool AwaitPromise { get; set; } = awaitPromise;
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
        if (r.Result is StringRemoteValue stringValue)
        {
            return stringValue.Value;
        }

        if (r.Result is NullRemoteValue)
        {
            return null;
        }

        throw new System.Exception($"Cannot convert {r.Result} to string");
    }
}

public class EvaluateResultException : EvaluateResult
{
    public ExceptionDetails ExceptionDetails { get; set; }
}

public class ExceptionDetails
{
    public uint ColumnNumber { get; set; }

    public uint LineNumber { get; set; }

    public StackTrace StackTrace { get; set; }

    public string Text { get; set; }
}
