namespace OpenQA.Selenium.BiDi.Internal;

internal class Notification
{

}

internal class Notification<T> : Notification
{
    public string? Type { get; set; }

    public int? Id { get; set; }

    public string? Method { get; set; }

    public T? Result { get; set; }

    public string? Error { get; set; }

    public string? Message { get; set; }

    public object? Params { get; set; }
}
