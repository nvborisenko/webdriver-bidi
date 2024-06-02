namespace OpenQA.Selenium.BiDi.Modules.Script;

public class ChannelValue : LocalValue
{
    public string Type => "channel";
}

public class ChannelProperties(Channel channel)
{
    public Channel Channel { get; } = channel;

    public SerializationOptions? SerializationOptions { get; set; }

    public ResultOwnership? Ownership { get; set; }
}