using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Script;

public abstract class RemoteValue
{
    public abstract string Type { get; }
}

public class NodeRemoteValue : RemoteValue
{
    public override string Type => "node";

    [JsonInclude]
    public string SharedId { get; private set; }
}
