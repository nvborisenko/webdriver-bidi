using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi
{
    public class EmptyResult
    {

    }

    public class Result<T> : EmptyResult
    {
        public string Type { get; set; }

        public int Id { get; set; }

        public string Method { get; set; }

        [JsonPropertyName("result")]
        public T ResultData { get; set; }

        public string Error { get; set; }

        [JsonPropertyName("message")]
        public string ErrorMessage { get; set; }

        public object Params { get; set; }
    }
}
