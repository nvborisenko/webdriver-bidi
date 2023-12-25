namespace OpenQA.Selenium.BiDi
{
    public abstract class Command
    {
        public int Id { get; set; }
    }

    public abstract class Command<TParameters> : Command
        where TParameters : new()
    {
        public abstract string Method { get; }

        public TParameters Params { get; set; } = new TParameters();
    }
}
