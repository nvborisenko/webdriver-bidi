﻿using OpenQA.Selenium.BiDi.Communication;
using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.Script;

internal class CallFunctionCommand(CallFunctionCommandParameters @params) : Command<CallFunctionCommandParameters>(@params);

internal class CallFunctionCommandParameters(string functionDeclaration, bool awaitPromise, Target target) : CommandParameters
{
    public string FunctionDeclaration { get; } = functionDeclaration;

    public bool AwaitPromise { get; } = awaitPromise;

    public Target Target { get; } = target;

    public IEnumerable<LocalValue>? Arguments { get; set; }
}
