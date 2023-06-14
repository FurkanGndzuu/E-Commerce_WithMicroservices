using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SharedService.Responses
{
    public class Response<T>
    {
        [JsonPropertyName("data")]
        public T Data { get; set; }
        [JsonIgnore]
        [JsonPropertyName("statusCodes")]
        public int StatusCode { get; set; }
        [JsonIgnore]
        [JsonPropertyName("isSuccess")]
        public bool isSuccess { get; set; }
        [JsonPropertyName("errors")]
        public List<string> Errors { get; set; }

        public static Response<T> Success(T data, int StatusCode) => new Response<T>()
        {
            Data = data,
            StatusCode = StatusCode,
            isSuccess = true
        };
        public static Response<T> Success(int StatusCode) => new Response<T>()
        {
            StatusCode = StatusCode,
            isSuccess = true
        };

        public static Response<T> Fail(string Error, int StatusCode) => new Response<T>()
        {
            StatusCode = StatusCode,
            Errors = new List<string>() { Error },
            isSuccess = false
        };

        public static Response<T> Fail(List<string> Errors, int StatusCode) => new Response<T>()
        {
            Errors = Errors,
            StatusCode = StatusCode,
            isSuccess = false
        };
    }
}
