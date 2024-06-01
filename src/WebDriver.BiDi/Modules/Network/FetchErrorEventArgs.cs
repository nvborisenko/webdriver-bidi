using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using System;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public class FetchErrorEventArgs : BaseParametersEventArgs
{
    [JsonConstructor]
    internal FetchErrorEventArgs(BrowsingContext.BrowsingContext context, bool isBlocked, Navigation navigation, uint redirectCount, RequestData request, DateTime timestamp, string errorText)
        : base(context, isBlocked, navigation, redirectCount, request, timestamp)
    {
        ErrorText = errorText;
    }

    public string ErrorText { get; }
}
