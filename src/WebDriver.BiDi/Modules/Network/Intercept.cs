namespace OpenQA.Selenium.BiDi.Modules.Network;

public class Intercept
{
    public Intercept(string id)
    {
        Id = id;
    }

    public string Id { get; private set; }

    public override bool Equals(object obj)
    {
        return Id == (obj as Intercept).Id;
    }
}
