using OpenQA.Selenium.BiDi.Communication;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Internal.Json
{
    [JsonSerializable(typeof(Command))]
    [JsonSerializable(typeof(Message))]

    [JsonSerializable(typeof(Modules.Session.StatusResult))]
    [JsonSerializable(typeof(Modules.Session.NewResult))]

    [JsonSerializable(typeof(Modules.Browser.CloseCommand),TypeInfoPropertyName = "Browser_CloseCommand")]
    [JsonSerializable(typeof(Modules.Browser.UserContextInfo))]
    [JsonSerializable(typeof(Modules.Browser.GetUserContextsResult))]

    [JsonSerializable(typeof(Modules.BrowsingContext.CloseCommand), TypeInfoPropertyName = "BrowsingContext_CloseCommand")]
    [JsonSerializable(typeof(Modules.BrowsingContext.CreateResult))]
    [JsonSerializable(typeof(Modules.BrowsingContext.BrowsingContextInfo))]
    [JsonSerializable(typeof(Modules.BrowsingContext.NavigateResult))]
    [JsonSerializable(typeof(Modules.BrowsingContext.NavigationInfoEventArgs))]
    [JsonSerializable(typeof(Modules.BrowsingContext.TraverseHistoryResult))]
    [JsonSerializable(typeof(Modules.BrowsingContext.LocateNodesResult))]
    [JsonSerializable(typeof(Modules.BrowsingContext.CaptureScreenshotResult))]
    [JsonSerializable(typeof(Modules.BrowsingContext.GetTreeResult))]
    [JsonSerializable(typeof(Modules.BrowsingContext.PrintResult))]
    [JsonSerializable(typeof(Modules.BrowsingContext.UserPromptOpenedEventArgs))]
    [JsonSerializable(typeof(Modules.BrowsingContext.UserPromptClosedEventArgs))]

    [JsonSerializable(typeof(Modules.Network.AddInterceptResult))]
    [JsonSerializable(typeof(Modules.Network.BeforeRequestSentEventArgs))]
    [JsonSerializable(typeof(Modules.Network.ResponseStartedEventArgs))]
    [JsonSerializable(typeof(Modules.Network.ResponseCompletedEventArgs))]
    [JsonSerializable(typeof(Modules.Network.FetchErrorEventArgs))]

    [JsonSerializable(typeof(Modules.Script.AddPreloadScriptResult))]
    [JsonSerializable(typeof(Modules.Script.EvaluateResult))]
    [JsonSerializable(typeof(Modules.Script.GetRealmsResult))]

    [JsonSerializable(typeof(Modules.Log.BaseLogEntryEventArgs))]

    [JsonSerializable(typeof(Modules.Storage.GetCookiesResult))]
    [JsonSerializable(typeof(Modules.Storage.DeleteCookiesResult))]
    [JsonSerializable(typeof(Modules.Storage.SetCookieResult))]
    internal partial class SourceGenerationContext : JsonSerializerContext
    {

    }
}
