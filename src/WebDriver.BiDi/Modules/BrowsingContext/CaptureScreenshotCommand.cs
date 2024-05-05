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

    public ClipRectangle? Clip { get; set; }
}

public enum Origin
{
    Viewport,
    Document
}

public class ImageFormat
{
    public ImageFormat(string type)
    {
        Type = type;
    }

    public string Type { get; set; }

    public double? Quality { get; set; }
}

[JsonDerivedType(typeof(BoxClipRectangle))]
[JsonDerivedType(typeof(ElementClipRectangle))]
public abstract class ClipRectangle
{
    public abstract string Type { get; }

    public static BoxClipRectangle Box(double x, double y, double width, double height)
        => new() { X = x, Y = y, Width = width, Height = height };
}

public class BoxClipRectangle : ClipRectangle
{
    public override string Type => "box";

    public double X { get; set; }

    public double Y { get; set; }

    public double Width { get; set; }

    public double Height { get; set; }
}

public class ElementClipRectangle : ClipRectangle
{
    public override string Type => "element";

    public Script.SharedReference Element { get; set; }
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