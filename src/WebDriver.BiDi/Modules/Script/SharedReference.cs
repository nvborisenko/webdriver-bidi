namespace OpenQA.Selenium.BiDi.Modules.Script;

public abstract class RemoteReference
{

}

public class SharedReference : RemoteReference
{
    public string SharedId { get; set; }
}

