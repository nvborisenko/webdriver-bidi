using OpenQA.Selenium.BiDi.Internal;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class CaptureScreenshotCommand(CaptureScreenshotCommandParameters @params) : Command<CaptureScreenshotCommandParameters>(@params);

internal class CaptureScreenshotCommandParameters(BrowsingContext context) : CommandParameters
{
    public BrowsingContext Context { get; } = context;

    public Origin? Origin { get; set; }

    public ImageFormat? Format { get; set; }

    public ClipRectangle? Clip { get; set; }
}

public enum Origin
{
    Viewport,
    Document
}

public class ImageFormat(string type)
{
    public string Type { get; set; } = type;

    public double? Quality { get; set; }
}

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(BoxClipRectangle), "box")]
[JsonDerivedType(typeof(ElementClipRectangle), "element")]
public abstract class ClipRectangle
{

}

public class BoxClipRectangle(double x, double y, double width, double height)
    : ClipRectangle
{
    public double X { get; } = x;

    public double Y { get; } = y;

    public double Width { get; } = width;

    public double Height { get; } = height;
}

public class ElementClipRectangle(Script.SharedReference element)
    : ClipRectangle
{
    public Script.SharedReference Element { get; } = element;
}

public class CaptureScreenshotResult(string data)
{
    public string Data { get; } = data;

    public byte[] AsBytes()
    {
        return System.Convert.FromBase64String(Data);
    }
}
