using System.Text.Json.Serialization;

namespace PhonebookMicroservices.Shared.ResponseTypes
{
    public class ApiResponse<T>
    {

        public DateTime TimeStamp { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string TraceId { get; set; } = null;
        public bool Success { get; set; }
        public string? Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T Data { get; set; }
        [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
        public object Errors { get; set; } = null;

        public static ApiResponse<T> Ok(T? data, string? message = null)
        {
            return new ApiResponse<T> { TimeStamp = DateTime.UtcNow,Success = true,Data = data, Message = message };
        }
        public static ApiResponse<T> Fail(string message, string traceId, object? errors = null)
        {
            return new ApiResponse<T> { TimeStamp = DateTime.UtcNow, Success = false, Message = message, Errors = errors,TraceId = traceId };
        }
    }
}
