using System;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi
{
    public abstract class Command
    {
        public long Id { get; set; }
    }

    public abstract class Command<TParameters> : Command
        where TParameters : new()
    {
        [JsonPropertyName("method")]
        public abstract string Name { get; }

        public TParameters Params { get; set; } = new TParameters();
    }
}
