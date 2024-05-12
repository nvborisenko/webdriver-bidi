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

public class Viewport
{
    public uint Width { get; set; }

    public uint Height { get; set; }
}
