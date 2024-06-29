using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Internal.Json
{
    [JsonSerializable(typeof(Message))]

    [JsonSerializable(typeof(Modules.Session.StatusCommand))]
    [JsonSerializable(typeof(Modules.Session.StatusResult))]
    [JsonSerializable(typeof(Modules.Session.SubscribeCommand))]
    [JsonSerializable(typeof(Modules.Session.UnsubscribeCommand))]
    [JsonSerializable(typeof(Modules.Session.NewCommand))]
    [JsonSerializable(typeof(Modules.Session.NewResult))]
    [JsonSerializable(typeof(Modules.Session.EndCommand))]

    [JsonSerializable(typeof(Modules.Browser.CreateUserContextCommand))]
    [JsonSerializable(typeof(Modules.Browser.UserContextInfo))]
    [JsonSerializable(typeof(Modules.Browser.GetUserContextsCommand))]
    [JsonSerializable(typeof(Modules.Browser.GetUserContextsResult))]
    [JsonSerializable(typeof(Modules.Browser.RemoveUserContextCommand))]

    [JsonSerializable(typeof(Modules.BrowsingContext.CreateCommand))]
    [JsonSerializable(typeof(Modules.BrowsingContext.CreateResult))]
    [JsonSerializable(typeof(Modules.BrowsingContext.BrowsingContextInfo))]
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
    [JsonSerializable(typeof(Modules.BrowsingContext.GetTreeCommand))]
    [JsonSerializable(typeof(Modules.BrowsingContext.GetTreeResult))]
    [JsonSerializable(typeof(Modules.BrowsingContext.PrintCommand))]
    [JsonSerializable(typeof(Modules.BrowsingContext.PrintResult))]
    [JsonSerializable(typeof(Modules.BrowsingContext.HandleUserPromptCommand))]
    [JsonSerializable(typeof(Modules.BrowsingContext.UserPromptOpenedEventArgs))]
    [JsonSerializable(typeof(Modules.BrowsingContext.UserPromptClosedEventArgs))]
    [JsonSerializable(typeof(Modules.BrowsingContext.CloseCommand))]

    [JsonSerializable(typeof(Modules.Network.AddInterceptCommand))]
    [JsonSerializable(typeof(Modules.Network.AddInterceptResult))]
    [JsonSerializable(typeof(Modules.Network.ContinueRequestCommand))]
    [JsonSerializable(typeof(Modules.Network.ContinueResponseCommand))]
    [JsonSerializable(typeof(Modules.Network.FailRequestCommand))]
    [JsonSerializable(typeof(Modules.Network.FailRequestCommand.Parameters))]
    [JsonSerializable(typeof(Modules.Network.ProvideResponseCommand))]
    [JsonSerializable(typeof(Modules.Network.RemoveInterceptCommand))]
    [JsonSerializable(typeof(Modules.Network.BeforeRequestSentEventArgs))]
    [JsonSerializable(typeof(Modules.Network.ResponseStartedEventArgs))]
    [JsonSerializable(typeof(Modules.Network.ResponseCompletedEventArgs))]
    [JsonSerializable(typeof(Modules.Network.FetchErrorEventArgs))]

    [JsonSerializable(typeof(Modules.Script.AddPreloadScriptCommand))]
    [JsonSerializable(typeof(Modules.Script.AddPreloadScriptResult))]
    [JsonSerializable(typeof(Modules.Script.RemovePreloadScriptCommand))]
    [JsonSerializable(typeof(Modules.Script.EvaluateCommand))]
    [JsonSerializable(typeof(Modules.Script.EvaluateResult))]
    [JsonSerializable(typeof(Modules.Script.CallFunctionCommand))]
    [JsonSerializable(typeof(Modules.Script.DisownCommand))]
    [JsonSerializable(typeof(Modules.Script.GetRealmsCommand))]
    [JsonSerializable(typeof(Modules.Script.GetRealmsResult))]


    [JsonSerializable(typeof(Modules.Input.PerformActionsCommand))]
    [JsonSerializable(typeof(Modules.Input.ReleaseActionsCommand))]

    [JsonSerializable(typeof(Modules.Log.BaseLogEntryEventArgs))]

    [JsonSerializable(typeof(Modules.Storage.GetCookiesCommand))]
    [JsonSerializable(typeof(Modules.Storage.GetCookiesResult))]
    [JsonSerializable(typeof(Modules.Storage.DeleteCookiesCommand))]
    [JsonSerializable(typeof(Modules.Storage.DeleteCookiesResult))]
    [JsonSerializable(typeof(Modules.Storage.SetCookieCommand))]
    [JsonSerializable(typeof(Modules.Storage.SetCookieResult))]
    internal partial class SourceGenerationContext : JsonSerializerContext
    {

    }
}
