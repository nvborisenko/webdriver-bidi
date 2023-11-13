using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext
{
    internal class CreateCommand : Command<CreateCommandParameters>
    {
        public override string Name { get; } = "browsingContext.create";
    }

    internal class CreateCommandParameters
    {
        public string Type { get; } = "tab";
    }
}
