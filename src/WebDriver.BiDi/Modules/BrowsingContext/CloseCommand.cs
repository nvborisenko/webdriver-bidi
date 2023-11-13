using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext
{
    internal class CloseCommand : Command<CloseCommandParameters>
    {
        public override string Name { get; } = "browsingContext.close";
    }

    internal class CloseCommandParameters
    {
        public string? Context { get; set; }
    }
}
