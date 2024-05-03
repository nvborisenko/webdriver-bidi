using OpenQA.Selenium.BiDi.Internal;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class CaptureScreenshotCommand : Command<CaptureScreenshotCommandParameters>
{
    public override string Method => "browsingContext.captureScreenshot";
}

public class CaptureScreenshotCommandParameters : EmptyCommandParameters
{
    public string Context { get; set; }

    public Origin? Origin { get; set; }

    public ImageFormat? Format { get; set; }
}

public enum Origin
{
    Viewport,
    Document
}

public class ImageFormat
{
    public string Type { get; set; }

    public double? Quality { get; set; }
}

public class CaptureScreenshotResult
{
    [JsonInclude]
    public string Data { get; private set; }

    public byte[] AsBytes()
    {
        return System.Convert.FromBase64String(Data);
    }
}