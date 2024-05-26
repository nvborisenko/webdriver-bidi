using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Internal.Json
{
    [JsonSerializable(typeof(Notification))]

    [JsonSerializable(typeof(Modules.Session.StatusCommand))]
    [JsonSerializable(typeof(Modules.Session.StatusResult))]
    [JsonSerializable(typeof(Modules.Session.SubscribeCommand))]
    [JsonSerializable(typeof(Modules.Session.EndCommand))]

    [JsonSerializable(typeof(Modules.BrowsingContext.CreateCommand))]
    [JsonSerializable(typeof(Modules.BrowsingContext.CreateResult))]
    [JsonSerializable(typeof(Modules.BrowsingContext.BrowsingContextInfoEventArgs))]
    [JsonSerializable(typeof(Modules.BrowsingContext.NavigateCommand))]
    [JsonSerializable(typeof(Modules.BrowsingContext.NavigateResult))]
    [JsonSerializable(typeof(Modules.BrowsingContext.NavigationInfoEventArgs))]
    [JsonSerializable(typeof(Modules.BrowsingContext.ReloadCommand))]
    [JsonSerializable(typeof(Modules.BrowsingContext.TraverseHistoryCommand))]
    [JsonSerializable(typeof(Modules.BrowsingContext.TraverseHistoryResult))]
    [JsonSerializable(typeof(Modules.BrowsingContext.ActivateCommand))]
    [JsonSerializable(typeof(Modules.BrowsingContext.LocateNodesCommand))]
    [JsonSerializable(typeof(Modules.BrowsingContext.LocateNodesResult))]
    [JsonSerializable(typeof(Modules.BrowsingContext.ActivateCommand))]
    [JsonSerializable(typeof(Modules.BrowsingContext.CaptureScreenshotCommand))]
    [JsonSerializable(typeof(Modules.BrowsingContext.CaptureScreenshotResult))]
    [JsonSerializable(typeof(Modules.BrowsingContext.SetViewportCommand))]
    [JsonSerializable(typeof(Modules.BrowsingContext.CloseCommand))]

    [JsonSerializable(typeof(Modules.Network.AddInterceptCommand))]
    [JsonSerializable(typeof(Modules.Network.AddInterceptResult))]
    [JsonSerializable(typeof(Modules.Network.ContinueRequestCommand))]
    [JsonSerializable(typeof(Modules.Network.ContinueResponseCommand))]
    [JsonSerializable(typeof(Modules.Network.FailRequestCommand))]
    [JsonSerializable(typeof(Modules.Network.ProvideResponseCommand))]
    [JsonSerializable(typeof(Modules.Network.RemoveInterceptCommand))]
    [JsonSerializable(typeof(Modules.Network.BeforeRequestSentEventArgs))]
    [JsonSerializable(typeof(Modules.Network.ResponseStartedEventArgs))]

    [JsonSerializable(typeof(Modules.Script.EvaluateCommand))]
    [JsonSerializable(typeof(Modules.Script.EvaluateResult))]

    [JsonSerializable(typeof(Modules.Input.PerformActionsCommand))]
    [JsonSerializable(typeof(Modules.Input.ReleaseActionsCommand))]
    internal partial class SourceGenerationContext : JsonSerializerContext
    {

    }
}
