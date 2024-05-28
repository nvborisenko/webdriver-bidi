using OpenQA.Selenium.BiDi.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class SetViewportCommand : Command<SetViewportParameters>
{
    public override string Method => "browsingContext.setViewport";
}

public class SetViewportParameters : CommandParameters
{
    public BrowsingContext Context { get; set; }

    public Viewport? Viewport { get; set; }

    public double? DevicePixelRatio { get; set; }
}

public class Viewport(uint width, uint height)
{
    public uint Width { get; } = width;

    public uint Height { get; } = height;
}
