using OpenQA.Selenium.BiDi.Communication;
using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.Script;

internal class CallFunctionCommand(CallFunctionCommandParameters @params) : Command<CallFunctionCommandParameters>(@params);

internal class CallFunctionCommandParameters(string functionDeclaration, bool awaitPromise, Target target) : CommandParameters
{
    public string FunctionDeclaration { get; } = functionDeclaration;

    public bool AwaitPromise { get; } = awaitPromise;

    public Target Target { get; } = target;

    public IEnumerable<LocalValue>? Arguments { get; set; }

    public ResultOwnership? ResultOwnership { get; set; }

    public SerializationOptions? SerializationOptions { get; set; }

    public LocalValue? This { get; set; }

    public bool? UserActivation { get; set; }
}

public class CallFunctionOptions : CommandOptions
{
    public IEnumerable<LocalValue>? Arguments { get; set; }

    public ResultOwnership? ResultOwnership { get; set; }

    public SerializationOptions? SerializationOptions { get; set; }

    public LocalValue? This { get; set; }

    public bool? UserActivation { get; set; }
}