using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace OpenQA.Selenium.BiDi.Communication.Json;

// https://github.com/dotnet/runtime/issues/72604
[JsonSerializable(typeof(MessageSuccess))]
[JsonSerializable(typeof(MessageError))]
[JsonSerializable(typeof(MessageEvent))]

[JsonSerializable(typeof(Modules.Script.EvaluateResultSuccess))]
[JsonSerializable(typeof(Modules.Script.EvaluateResultException))]

[JsonSerializable(typeof(Modules.Script.NumberRemoteValue))]
[JsonSerializable(typeof(Modules.Script.StringRemoteValue))]
[JsonSerializable(typeof(Modules.Script.NullRemoteValue))]
[JsonSerializable(typeof(Modules.Script.UndefinedRemoteValue))]
[JsonSerializable(typeof(Modules.Script.SymbolRemoteValue))]
[JsonSerializable(typeof(Modules.Script.ObjectRemoteValue))]
[JsonSerializable(typeof(Modules.Script.FunctionRemoteValue))]
[JsonSerializable(typeof(Modules.Script.RegExpRemoteValue))]
[JsonSerializable(typeof(Modules.Script.DateRemoteValue))]
[JsonSerializable(typeof(Modules.Script.MapRemoteValue))]
[JsonSerializable(typeof(Modules.Script.SetRemoteValue))]
[JsonSerializable(typeof(Modules.Script.WeakMapRemoteValue))]
[JsonSerializable(typeof(Modules.Script.WeakSetRemoteValue))]
[JsonSerializable(typeof(Modules.Script.GeneratorRemoteValue))]
[JsonSerializable(typeof(Modules.Script.ErrorRemoteValue))]
[JsonSerializable(typeof(Modules.Script.ProxyRemoteValue))]
[JsonSerializable(typeof(Modules.Script.PromiseRemoteValue))]
[JsonSerializable(typeof(Modules.Script.TypedArrayRemoteValue))]
[JsonSerializable(typeof(Modules.Script.ArrayBufferRemoteValue))]
[JsonSerializable(typeof(Modules.Script.NodeListRemoteValue))]
[JsonSerializable(typeof(Modules.Script.HtmlCollectionRemoteValue))]
[JsonSerializable(typeof(Modules.Script.NodeRemoteValue))]
[JsonSerializable(typeof(Modules.Script.WindowProxyRemoteValue))]

[JsonSerializable(typeof(Modules.Script.WindowRealmInfo))]
[JsonSerializable(typeof(Modules.Script.DedicatedWorkerRealmInfo))]
[JsonSerializable(typeof(Modules.Script.SharedWorkerRealmInfo))]
[JsonSerializable(typeof(Modules.Script.ServiceWorkerRealmInfo))]
[JsonSerializable(typeof(Modules.Script.WorkerRealmInfo))]
[JsonSerializable(typeof(Modules.Script.PaintWorkletRealmInfo))]
[JsonSerializable(typeof(Modules.Script.AudioWorkletRealmInfo))]
[JsonSerializable(typeof(Modules.Script.WorkletRealmInfo))]

[JsonSerializable(typeof(Modules.Log.ConsoleLogEntry))]
[JsonSerializable(typeof(Modules.Log.JavascriptLogEntry))]
//

[JsonSerializable(typeof(Command))]
[JsonSerializable(typeof(Message))]

[JsonSerializable(typeof(Modules.Session.StatusResult))]
[JsonSerializable(typeof(Modules.Session.NewResult))]

[JsonSerializable(typeof(Modules.Browser.CloseCommand), TypeInfoPropertyName = "Browser_CloseCommand")]
[JsonSerializable(typeof(Modules.Browser.UserContextInfo))]
[JsonSerializable(typeof(Modules.Browser.GetUserContextsResult))]

[JsonSerializable(typeof(Modules.BrowsingContext.CloseCommand), TypeInfoPropertyName = "BrowsingContext_CloseCommand")]
[JsonSerializable(typeof(Modules.BrowsingContext.CreateResult))]
[JsonSerializable(typeof(Modules.BrowsingContext.BrowsingContextInfo))]
[JsonSerializable(typeof(Modules.BrowsingContext.NavigateResult))]
[JsonSerializable(typeof(Modules.BrowsingContext.NavigationInfo))]
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

[JsonSerializable(typeof(Modules.Log.BaseLogEntry))]

[JsonSerializable(typeof(Modules.Storage.GetCookiesResult))]
[JsonSerializable(typeof(Modules.Storage.DeleteCookiesResult))]
[JsonSerializable(typeof(Modules.Storage.SetCookieResult))]
internal partial class SourceGenerationContext : JsonSerializerContext;
