using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class SetViewportCommand(SetViewportParameters parameters) : Command<SetViewportParameters>("browsingContext.setViewport", parameters)
{

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
