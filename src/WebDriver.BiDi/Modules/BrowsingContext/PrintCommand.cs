using OpenQA.Selenium.BiDi.Internal;
using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class PrintCommand(PrintCommand.Parameters @params)
    : Command<PrintCommand.Parameters>("browsingContext.print", @params)
{
    internal class Parameters(BrowsingContext context) : CommandParameters
    {
        public BrowsingContext Context { get; } = context;

        public bool? Background { get; set; }

        public Margin? Margin { get; set; }

        public Orientation? Orientation { get; set; }

        public Page? Page { get; set; }

        // TODO: It also supports strings
        public IEnumerable<uint>? PageRanges { get; set; }

        public double? Scale { get; set; }

        public bool? ShrinkToFit { get; set; }
    }
}

public struct Margin
{
    public double? Bottom { get; set; }

    public double? Left { get; set; }

    public double? Right { get; set; }

    public double? Top { get; set; }
}

public enum Orientation
{
    Portrait,
    Landscape
}

public struct Page
{
    public double? Height { get; set; }

    public double? Width { get; set; }
}

internal class PrintResult(string data)
{
    public string Data { get; } = data;
}