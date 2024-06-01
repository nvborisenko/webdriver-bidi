using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Storage;

internal class DeleteCookiesCommand(DeleteCookiesCommand.Parameters @params)
    : Command<DeleteCookiesCommand.Parameters>("storage.deleteCookies", @params)
{
    internal class Parameters : CommandParameters
    {
        public CookieFilter? Filter { get; set; }

        public PartitionDescriptor? Partition { get; set; }
    }
}

public class DeleteCookiesResult(PartitionKey partitionKey)
{
    public PartitionKey PartitionKey { get; } = partitionKey;
}
