using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi;

public class Result
{

}

public class Result<T> : Result
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
