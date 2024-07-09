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
public abstract record RealmInfo : EventArgs;

public abstract record BaseRealmInfo(Realm Realm, string Origin) : RealmInfo;

public record WindowRealmInfo(Realm Realm, string Origin, BrowsingContext.BrowsingContext Context) : BaseRealmInfo(Realm, Origin)
{
    public string? Sandbox { get; set; }
}

public record DedicatedWorkerRealmInfo(Realm Realm, string Origin, IReadOnlyList<Realm> Owners) : BaseRealmInfo(Realm, Origin);

public record SharedWorkerRealmInfo(Realm Realm, string Origin) : BaseRealmInfo(Realm, Origin);

public record ServiceWorkerRealmInfo(Realm realm, string origin) : BaseRealmInfo(realm, origin);

public record WorkerRealmInfo(Realm realm, string origin) : BaseRealmInfo(realm, origin);

public record PaintWorkletRealmInfo(Realm realm, string origin) : BaseRealmInfo(realm, origin);

public record AudioWorkletRealmInfo(Realm realm, string origin) : BaseRealmInfo(realm, origin);

public record WorkletRealmInfo(Realm realm, string origin) : BaseRealmInfo(realm, origin);