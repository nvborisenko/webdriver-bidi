namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext
{
    internal class CreateCommand : Command<CreateCommandParameters>
    {
        public override string Name { get; } = "browsingContext.create";
    }

    internal class CreateCommandParameters
    {
        public BrowsingContextType Type { get; } = BrowsingContextType.Tab;
    }

    internal enum BrowsingContextType
    {
        Tab,
        Window
    }
}
