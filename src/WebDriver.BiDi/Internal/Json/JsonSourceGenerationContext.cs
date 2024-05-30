﻿using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Internal.Json
{
    [JsonSerializable(typeof(Message))]

    [JsonSerializable(typeof(Modules.Session.StatusCommand))]
    [JsonSerializable(typeof(Modules.Session.StatusResult))]
    [JsonSerializable(typeof(Modules.Session.SubscribeCommand))]
    [JsonSerializable(typeof(Modules.Session.SubscribeCommand.Parameters), TypeInfoPropertyName = "Session_SubscribeCommand_Parameters")]
    [JsonSerializable(typeof(Modules.Session.EndCommand))]

    [JsonSerializable(typeof(Modules.Browser.CreateUserContextCommand))]
    [JsonSerializable(typeof(Modules.Browser.UserContextInfo))]
    [JsonSerializable(typeof(Modules.Browser.GetUserContextsCommand))]
    [JsonSerializable(typeof(Modules.Browser.GetUserContextsResult))]
    [JsonSerializable(typeof(Modules.Browser.RemoveUserContextCommand))]
    [JsonSerializable(typeof(Modules.Browser.RemoveUserContextCommand.Parameters), TypeInfoPropertyName = "Browser_RemoveUserContextCommand_Parameters")]

    [JsonSerializable(typeof(Modules.BrowsingContext.CreateCommand))]
    [JsonSerializable(typeof(Modules.BrowsingContext.CreateCommand.Parameters), TypeInfoPropertyName = "BrowsingContext_CreateCommand_Parameters")]
    [JsonSerializable(typeof(Modules.BrowsingContext.CreateResult))]
    [JsonSerializable(typeof(Modules.BrowsingContext.BrowsingContextInfo))]
    [JsonSerializable(typeof(Modules.BrowsingContext.NavigateCommand))]
    [JsonSerializable(typeof(Modules.BrowsingContext.NavigateCommand.Parameters), TypeInfoPropertyName = "BrowsingContext_NavigateCommand_Parameters")]
    [JsonSerializable(typeof(Modules.BrowsingContext.NavigateResult))]
    [JsonSerializable(typeof(Modules.BrowsingContext.NavigationInfoEventArgs))]
    [JsonSerializable(typeof(Modules.BrowsingContext.ReloadCommand))]
    [JsonSerializable(typeof(Modules.BrowsingContext.ReloadCommand.Parameters), TypeInfoPropertyName = "BrowsingContext_ReloadCommand_Parameters")]
    [JsonSerializable(typeof(Modules.BrowsingContext.TraverseHistoryCommand))]
    [JsonSerializable(typeof(Modules.BrowsingContext.TraverseHistoryCommand.Parameters), TypeInfoPropertyName = "BrowsingContext_TraverseHistoryCommand_Parameters")]
    [JsonSerializable(typeof(Modules.BrowsingContext.TraverseHistoryResult))]
    [JsonSerializable(typeof(Modules.BrowsingContext.ActivateCommand))]
    [JsonSerializable(typeof(Modules.BrowsingContext.ActivateCommand.Parameters), TypeInfoPropertyName = "BrowsingContext_ActivateCommand_Parameters")]
    [JsonSerializable(typeof(Modules.BrowsingContext.LocateNodesCommand))]
    [JsonSerializable(typeof(Modules.BrowsingContext.LocateNodesCommand.Parameters), TypeInfoPropertyName = "BrowsingContext_LocateNodesCommand_Parameters")]
    [JsonSerializable(typeof(Modules.BrowsingContext.LocateNodesResult))]
    [JsonSerializable(typeof(Modules.BrowsingContext.ActivateCommand))]
    [JsonSerializable(typeof(Modules.BrowsingContext.ActivateCommand.Parameters), TypeInfoPropertyName = "BrowsingContext_ActivateCommand_Parameters")]
    [JsonSerializable(typeof(Modules.BrowsingContext.CaptureScreenshotCommand))]
    [JsonSerializable(typeof(Modules.BrowsingContext.CaptureScreenshotCommand.Parameters), TypeInfoPropertyName = "BrowsingContext_CaptureScreenshotCommand_Parameters")]
    [JsonSerializable(typeof(Modules.BrowsingContext.CaptureScreenshotResult))]
    [JsonSerializable(typeof(Modules.BrowsingContext.SetViewportCommand))]
    [JsonSerializable(typeof(Modules.BrowsingContext.SetViewportCommand.Parameters), TypeInfoPropertyName = "BrowsingContext_SetViewportCommand_Parameters")]
    [JsonSerializable(typeof(Modules.BrowsingContext.GetTreeCommand))]
    [JsonSerializable(typeof(Modules.BrowsingContext.GetTreeCommand.Parameters), TypeInfoPropertyName = "BrowsingContext_GetTreeCommand_Parameters")]
    [JsonSerializable(typeof(Modules.BrowsingContext.GetTreeResult))]
    [JsonSerializable(typeof(Modules.BrowsingContext.PrintCommand))]
    [JsonSerializable(typeof(Modules.BrowsingContext.PrintCommand.Parameters), TypeInfoPropertyName = "BrowsingContext_PrintCommand_Parameters")]
    [JsonSerializable(typeof(Modules.BrowsingContext.PrintResult))]
    [JsonSerializable(typeof(Modules.BrowsingContext.CloseCommand))]
    [JsonSerializable(typeof(Modules.BrowsingContext.CloseCommand.Parameters), TypeInfoPropertyName = "BrowsingContext_CloseCommand_Parameters")]

    [JsonSerializable(typeof(Modules.Network.AddInterceptCommand))]
    [JsonSerializable(typeof(Modules.Network.AddInterceptCommand.Parameters), TypeInfoPropertyName = "Network_AddInterceptCommand_Parameters")]
    [JsonSerializable(typeof(Modules.Network.AddInterceptResult))]
    [JsonSerializable(typeof(Modules.Network.ContinueRequestCommand))]
    [JsonSerializable(typeof(Modules.Network.ContinueRequestCommand.Parameters), TypeInfoPropertyName = "Network_ContinueRequestCommand_Parameters")]
    [JsonSerializable(typeof(Modules.Network.ContinueResponseCommand))]
    [JsonSerializable(typeof(Modules.Network.ContinueResponseCommand.Parameters), TypeInfoPropertyName = "Network_ContinueResponseCommand_Parameters")]
    [JsonSerializable(typeof(Modules.Network.FailRequestCommand))]
    [JsonSerializable(typeof(Modules.Network.FailRequestCommand.Parameters))]
    [JsonSerializable(typeof(Modules.Network.ProvideResponseCommand))]
    [JsonSerializable(typeof(Modules.Network.ProvideResponseCommand.Parameters), TypeInfoPropertyName = "Network_ProvideResponseCommand_Parameters")]
    [JsonSerializable(typeof(Modules.Network.RemoveInterceptCommand))]
    [JsonSerializable(typeof(Modules.Network.RemoveInterceptCommand.Parameters), TypeInfoPropertyName = "Network_RemoveInterceptCommand_Parameters")]
    [JsonSerializable(typeof(Modules.Network.BeforeRequestSentEventArgs))]
    [JsonSerializable(typeof(Modules.Network.ResponseStartedEventArgs))]

    [JsonSerializable(typeof(Modules.Script.EvaluateCommand))]
    [JsonSerializable(typeof(Modules.Script.EvaluateCommand.Parameters), TypeInfoPropertyName = "Script_EvaluateCommand_Parameters")]
    [JsonSerializable(typeof(Modules.Script.EvaluateResult))]

    [JsonSerializable(typeof(Modules.Input.PerformActionsCommand))]
    [JsonSerializable(typeof(Modules.Input.PerformActionsCommand.Parameters), TypeInfoPropertyName = "Input_PerformActionsCommand_Parameters")]
    [JsonSerializable(typeof(Modules.Input.ReleaseActionsCommand))]
    [JsonSerializable(typeof(Modules.Input.ReleaseActionsCommand.Parameters), TypeInfoPropertyName = "Input_ReleaseActionsCommand_Parameters")]

    [JsonSerializable(typeof(Modules.Log.LogEntryEventArgs))]
    internal partial class SourceGenerationContext : JsonSerializerContext
    {

    }
}