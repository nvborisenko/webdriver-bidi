using System.Collections.Generic;
using System.Text.Json.Serialization;
using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Network
{
    internal class AddInterceptCommand : Command<AddInterceptParameters>
    {
        public override string Method { get; } = "network.addIntercept";
    }

    public class AddInterceptParameters : EmptyCommandParameters
    {
        public List<InterceptPhase> Phases { get; set; } = [];

        public List<UrlPattern>? UrlPatterns { get; set; } = [];
    }

    public enum InterceptPhase
    {
        BeforeRequestSent,
        ResponseStarted,
        AuthRequired
    }

    [JsonDerivedType(typeof(UrlPatternString))]
    public abstract class UrlPattern
    {
        public abstract string Type { get; }

        public static UrlPatternString String(string pattern)
        {
            return new UrlPatternString { Pattern = pattern };
        }
    }

    public class UrlPatternString : UrlPattern
    {
        public override string Type { get; } = "string";

        public string Pattern { get; set; }
    }
}
