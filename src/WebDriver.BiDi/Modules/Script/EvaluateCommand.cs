using OpenQA.Selenium.BiDi.Internal;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Script;

internal class EvaluateCommand : Command<EvaluateCommandParameters>
{
    public override string Method => "script.evaluate";
}

public class EvaluateCommandParameters : CommandParameters
{
    public string Expression { get; set; }

    public Target Target { get; set; }

    public bool AwaitPromise { get; set; }
}

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(EvaluateResultSuccess), "success")]
[JsonDerivedType(typeof(EvaluateResultException), "exception")]
public abstract class EvaluateResult
{
    public static implicit operator int(EvaluateResult r) => (int)(AsSuccess(r).Result as NumberValue).Value;
    public static implicit operator long(EvaluateResult r) => (AsSuccess(r).Result as NumberValue).Value;

    public static implicit operator string(EvaluateResult r)
    {
        var successResult = AsSuccess(r).Result;

        if (successResult is StringValue stringValue)
        {
            return stringValue.Value;
        }

        if (successResult is NullValue)
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