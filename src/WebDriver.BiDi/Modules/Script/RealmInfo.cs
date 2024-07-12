using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.Script;

// https://github.com/dotnet/runtime/issues/72604
//[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
//[JsonDerivedType(typeof(WindowRealmInfo), "window")]
//[JsonDerivedType(typeof(DedicatedWorkerRealmInfo), "dedicated-worker")]
//[JsonDerivedType(typeof(SharedWorkerRealmInfo), "shared-worker")]
//[JsonDerivedType(typeof(ServiceWorkerRealmInfo), "service-worker")]
//[JsonDerivedType(typeof(WorkerRealmInfo), "worker")]
//[JsonDerivedType(typeof(PaintWorkletRealmInfo), "paint-worklet")]
//[JsonDerivedType(typeof(AudioWorkletRealmInfo), "audio-worklet")]
//[JsonDerivedType(typeof(WorkletRealmInfo), "worklet")]
public abstract record RealmInfo(BiDi.Session Session) : EventArgs(Session);

public abstract record BaseRealmInfo(BiDi.Session Session, Realm Realm, string Origin) : RealmInfo(Session);

public record WindowRealmInfo(BiDi.Session Session, Realm Realm, string Origin, BrowsingContext.BrowsingContext Context) : BaseRealmInfo(Session, Realm, Origin)
{
    public string? Sandbox { get; set; }
}

public record DedicatedWorkerRealmInfo(BiDi.Session Session, Realm Realm, string Origin, IReadOnlyList<Realm> Owners) : BaseRealmInfo(Session, Realm, Origin);

public record SharedWorkerRealmInfo(BiDi.Session Session, Realm Realm, string Origin) : BaseRealmInfo(Session, Realm, Origin);

public record ServiceWorkerRealmInfo(BiDi.Session Session, Realm Realm, string Origin) : BaseRealmInfo(Session, Realm, Origin);

public record WorkerRealmInfo(BiDi.Session Session, Realm Realm, string Origin) : BaseRealmInfo(Session, Realm, Origin);

public record PaintWorkletRealmInfo(BiDi.Session Session, Realm Realm, string Origin) : BaseRealmInfo(Session, Realm, Origin);

public record AudioWorkletRealmInfo(BiDi.Session Session, Realm Realm, string Origin) : BaseRealmInfo(Session, Realm, Origin);

public record WorkletRealmInfo(BiDi.Session Session, Realm Realm, string Origin) : BaseRealmInfo(Session, Realm, Origin);
