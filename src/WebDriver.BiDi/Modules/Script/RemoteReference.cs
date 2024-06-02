namespace OpenQA.Selenium.BiDi.Modules.Script;

public abstract class RemoteReference : LocalValue
{

}

public class SharedReference(string sharedId) : RemoteReference
{
    public string SharedId { get; } = sharedId;

    public Handle? Handle { get; set; }
}

public class RemoteObjectReference(Handle handle) : RemoteReference
{
    public Handle Handle { get; } = handle;

    public string? SharedId { get; set; }
}