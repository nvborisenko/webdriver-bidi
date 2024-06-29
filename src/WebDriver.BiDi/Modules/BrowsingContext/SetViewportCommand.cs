using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class SetViewportCommand(SetViewportCommandParameters @params)
    : Command<SetViewportCommandParameters>("browsingContext.setViewport", @params)
{
    
}

internal class SetViewportCommandParameters(BrowsingContext context) : CommandParameters
{
    public BrowsingContext Context { get; } = context;

    public Viewport? Viewport { get; set; }

    public double? DevicePixelRatio { get; set; }
}

public readonly struct Viewport(uint width, uint height)
{
    public uint Width { get; } = width;

    public uint Height { get; } = height;
}
