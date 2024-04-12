using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Internal;

internal class Result
{

}

internal class Result<T> : Result
{
    public string? Type { get; set; }

    public int? Id { get; set; }

    public string? Method { get; set; }

    [JsonPropertyName("result")]
    public T? ResultData { get; set; }

    public string? Error { get; set; }

    public string? Message { get; set; }

    public object? Params { get; set; }
}
