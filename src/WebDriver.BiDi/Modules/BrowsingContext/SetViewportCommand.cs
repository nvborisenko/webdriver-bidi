using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class SetViewportCommand(SetViewportCommand.Parameters @params)
    : Command<SetViewportCommand.Parameters>("browsingContext.setViewport", @params)
{
    internal class Parameters : CommandParameters
    {
        public BrowsingContext Context { get; set; }

        public Viewport? Viewport { get; set; }

        public double? DevicePixelRatio { get; set; }
    }
}

public readonly struct Viewport(uint width, uint height)
{
    public uint Width { get; } = width;

    public uint Height { get; } = height;
}
