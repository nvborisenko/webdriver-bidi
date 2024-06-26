﻿using OpenQA.Selenium.BiDi.Communication;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal class RemoveInterceptCommand(RemoveInterceptCommandParameters @params) : Command<RemoveInterceptCommandParameters>(@params);

internal class RemoveInterceptCommandParameters(Intercept intercept) : CommandParameters
{
    public Intercept Intercept { get; } = intercept;
}
