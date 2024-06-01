namespace OpenQA.Selenium.BiDi.Modules.Script;

public class SerializationOptions
{
    public uint? MaxDomDepth { get; set; }

    public uint? MaxObjectDepth { get; set; }

    public ShadowTree? IncludeShadowTree { get; set; }
}

public enum ShadowTree
{
    None,
    Open,
    All
}