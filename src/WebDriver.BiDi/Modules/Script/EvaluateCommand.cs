using OpenQA.Selenium.BiDi.Internal;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Script;

internal class EvaluateCommand(EvaluateCommand.Parameters @params)
    : Command<EvaluateCommand.Parameters>("script.evaluate", @params)
{
    internal class Parameters(string expression, Target target, bool awaitPromise) : CommandParameters
    {
        public string Expression { get; set; } = expression;

        public Target Target { get; set; } = target;

        public bool AwaitPromise { get; set; } = awaitPromise;
    }
}

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(EvaluateResultSuccess), "success")]
[JsonDerivedType(typeof(EvaluateResultException), "exception")]
public abstract class EvaluateResult
{
    public static implicit operator int(EvaluateResult r) => (int)(AsSuccess(r).Result as NumberRemoteValue).Value;
    public static implicit operator long(EvaluateResult r) => (AsSuccess(r).Result as NumberRemoteValue).Value;
    public static implicit operator NodeRemoteValue(EvaluateResult r) => AsSuccess(r).Result as NodeRemoteValue;

    public static implicit operator string(EvaluateResult r)
    {
        var successResult = AsSuccess(r).Result;

        if (successResult is StringRemoteValue stringValue)
        {
            return stringValue.Value;
        }

        if (successResult is NullRemoteValue)
        {
            return null;
        }

        throw new System.Exception($"Cannot convert {successResult} to string");
    }

    private static EvaluateResultSuccess AsSuccess(EvaluateResult evaluateResult)
    {
        if (evaluateResult is EvaluateResultException exception)
        {
            throw new System.Exception(exception.ExceptionDetails.Text);
        }

        return (EvaluateResultSuccess)evaluateResult;
    }
}

public class EvaluateResultSuccess : EvaluateResult
{
    public RemoteValue Result { get; set; }
}

public class EvaluateResultException : EvaluateResult
{
    public ExceptionDetails ExceptionDetails { get; set; }
}

public class ExceptionDetails
{
    public uint ColumnNumber { get; set; }

    public string Text { get; set; }
}