using OpenQA.Selenium.BiDi.Internal;
using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.Script;

internal class CallFunctionCommand(CallFunctionCommand.Parameters @params)
    : Command<CallFunctionCommand.Parameters>("script.callFunction", @params)
{
    internal class Parameters(string functionDeclaration, bool awaitPromise, Target target) : CommandParameters
    {
        public string FunctionDeclaration { get; } = functionDeclaration;

        public bool AwaitPromise { get; } = awaitPromise;

        public Target Target { get; } = target;

        public IEnumerable<LocalValue>? Arguments { get; set; }
    }
}
