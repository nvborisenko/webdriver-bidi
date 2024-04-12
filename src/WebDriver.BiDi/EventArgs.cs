namespace OpenQA.Selenium.BiDi
{
    public abstract class EventArgs : System.EventArgs
    {
        public BiDiSession Session { get; internal set; }
    }
}
