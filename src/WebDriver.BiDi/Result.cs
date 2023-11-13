using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi
{
    public class EmptyResult
    {

    }

    public class Result<T> : EmptyResult
    {
        public string Type { get; set; }

        public long Id { get; set; }

        [JsonPropertyName("result")]
        public T ResultData { get; set; }

        public string Error { get; set; }

        [JsonPropertyName("message")]
        public string ErrorMessage { get; set; }
    }
}
