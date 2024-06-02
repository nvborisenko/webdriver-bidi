using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Script;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(WindowRealmInfo), "window")]
[JsonDerivedType(typeof(DedicatedWorkerRealmInfo), "dedicated-worker")]
[JsonDerivedType(typeof(SharedWorkerRealmInfo), "shared-worker")]
[JsonDerivedType(typeof(ServiceWorkerRealmInfo), "service-worker")]
[JsonDerivedType(typeof(WorkerRealmInfo), "worker")]
[JsonDerivedType(typeof(PaintWorkletRealmInfo), "paint-worklet")]
[JsonDerivedType(typeof(AudioWorkletRealmInfo), "audio-worklet")]
[JsonDerivedType(typeof(WorkletRealmInfo), "worklet")]
public abstract class RealmInfoEventArgs : EventArgs
{
}

public abstract class BaseRealmInfo(Realm realm, string origin) : RealmInfoEventArgs
{
    public Realm Realm { get; } = realm;

    public string Origin { get; } = origin;
}

public class WindowRealmInfo : BaseRealmInfo
{
    public WindowRealmInfo(Realm realm, string origin, BrowsingContext.BrowsingContext context)
        : base(realm, origin)
    {
        Context = context;
    }

    public BrowsingContext.BrowsingContext Context { get; }

    public string? Sandbox { get; set; }
}

public class DedicatedWorkerRealmInfo : BaseRealmInfo
{
    public DedicatedWorkerRealmInfo(Realm realm, string origin, IReadOnlyList<Realm> owners)
        : base(realm, origin)
    {
        Owners = owners;
    }

    public IReadOnlyList<Realm> Owners { get; }
}

public class SharedWorkerRealmInfo : BaseRealmInfo
{
    public SharedWorkerRealmInfo(Realm realm, string origin)
        : base(realm, origin)
    {

    }
}

public class ServiceWorkerRealmInfo : BaseRealmInfo
{
    public ServiceWorkerRealmInfo(Realm realm, string origin)
        : base(realm, origin)
    {

    }
}

public class WorkerRealmInfo : BaseRealmInfo
{
    public WorkerRealmInfo(Realm realm, string origin)
        : base(realm, origin)
    {

    }
}

public class PaintWorkletRealmInfo : BaseRealmInfo
{
    public PaintWorkletRealmInfo(Realm realm, string origin)
        : base(realm, origin)
    {

    }
}

public class AudioWorkletRealmInfo : BaseRealmInfo
{
    public AudioWorkletRealmInfo(Realm realm, string origin)
        : base(realm, origin)
    {

    }
}

public class WorkletRealmInfo : BaseRealmInfo
{
    public WorkletRealmInfo(Realm realm, string origin)
        : base(realm, origin)
    {

    }
}