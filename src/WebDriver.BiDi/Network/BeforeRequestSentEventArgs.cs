using System;

namespace OpenQA.Selenium.BiDi.Network;

public class BeforeRequestSentEventArgs : EventArgs
{
    public bool IsBlocked { get; set; }

    public Request Request { get; set; }
}

public class Request
{
    public string Url { get; set; }
}
