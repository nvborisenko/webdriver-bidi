using OpenQA.Selenium.BiDi.Internal;
using System;

namespace OpenQA.Selenium.BiDi.Modules.Storage;

internal class SetCookieCommand(SetCookieCommandParameters @params) : Command<SetCookieCommandParameters>(@params);

internal class SetCookieCommandParameters(PartialCookie partialCookie) : CommandParameters
{
    public PartialCookie Cookie { get; } = partialCookie;

    public PartitionDescriptor? Partition { get; set; }
}

public class PartialCookie(string name, Network.BytesValue value, string domain)
{
    public string Name { get; } = name;

    public Network.BytesValue Value { get; } = value;

    public string Domain { get; } = domain;

    public string? Path { get; set; }

    public bool? HttpOnly { get; set; }

    public bool? Secure { get; set; }

    public Network.SameSite? SameSite { get; set; }

    public DateTime? Expiry { get; set; }
}

public class SetCookieResult(PartitionKey partitionKey)
{
    public PartitionKey PartitionKey { get; } = partitionKey;
}
