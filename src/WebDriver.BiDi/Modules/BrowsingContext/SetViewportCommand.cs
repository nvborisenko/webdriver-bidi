﻿using OpenQA.Selenium.BiDi.Communication;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class SetViewportCommand(SetViewportCommandParameters @params) : Command<SetViewportCommandParameters>(@params);

internal record SetViewportCommandParameters(BrowsingContext Context) : CommandParameters
{
    public Viewport? Viewport { get; set; }

    public double? DevicePixelRatio { get; set; }
}

public record ViewportOptions : CommandOptions
{
    public Viewport? Viewport { get; set; }

    public double? DevicePixelRatio { get; set; }
}

public readonly record struct Viewport(ulong Width, ulong Height);
