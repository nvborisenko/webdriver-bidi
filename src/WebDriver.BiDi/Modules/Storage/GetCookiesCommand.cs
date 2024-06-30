using OpenQA.Selenium.BiDi.Internal;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Storage;

internal class GetCookiesCommand(GetCookiesCommandParameters @params) : Command<GetCookiesCommandParameters>(@params);

internal class GetCookiesCommandParameters : CommandParameters
{
    public CookieFilter? Filter { get; set; }

    public PartitionDescriptor? Partition { get; set; }
}

public class GetCookiesResult(IReadOnlyList<Network.Cookie> cookies, PartitionKey partitionKey)
{
    public IReadOnlyList<Network.Cookie> Cookies { get; } = cookies;

    public PartitionKey PartitionKey { get; } = partitionKey;
}

public class CookieFilter
{
    public string? Name { get; set; }

    public Network.BytesValue? Value { get; set; }

    public string? Domain { get; set; }

    public string? Path { get; set; }

    public uint? Size { get; set; }

    public bool? HttpOnly { get; set; }

    public bool? Secure { get; set; }

    public Network.SameSite? SameSite { get; set; }

    public DateTime? Expiry { get; set; }
}

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(BrowsingContextPartitionDescriptor), "context")]
[JsonDerivedType(typeof(StorageKeyPartitionDescriptor), "storageKey")]
public abstract class PartitionDescriptor
{

}

public class BrowsingContextPartitionDescriptor(BrowsingContext.BrowsingContext context) : PartitionDescriptor
{
    public BrowsingContext.BrowsingContext Context { get; } = context;
}

public class StorageKeyPartitionDescriptor : PartitionDescriptor
{
    public string? UserContext { get; set; }

    public string? SourceOrigin { get; set; }
}
