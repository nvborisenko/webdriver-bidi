using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Storage;

internal class DeleteCookiesCommand(DeleteCookiesCommandParameters @params) : Command<DeleteCookiesCommandParameters>(@params);

internal class DeleteCookiesCommandParameters : CommandParameters
{
    public CookieFilter? Filter { get; set; }

    public PartitionDescriptor? Partition { get; set; }
}

public class DeleteCookiesResult(PartitionKey partitionKey)
{
    public PartitionKey PartitionKey { get; } = partitionKey;
}
