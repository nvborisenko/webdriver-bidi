using Newtonsoft.Json;

namespace OpenQA.Selenium.BiDi
{
    public class EmptyResult
    {

    }
    public class Result<T> : EmptyResult
    {
        public string Type { get; set; }

        public long Id { get; set; }

        [JsonProperty("result")]
        public T ResultData { get; set; }

        public string Error { get; set; }

        [JsonProperty("message")]
        public string ErrorMessage { get; set; }
    }
}
