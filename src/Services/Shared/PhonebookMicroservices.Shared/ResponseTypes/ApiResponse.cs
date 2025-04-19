﻿namespace PhonebookMicroservices.Shared.ResponseTypes
{
    public class ApiResponse<T>
    {
        public DateTime TimeStamp { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public object Errors { get; set; }

        public static ApiResponse<T> Ok(T? data, string? message = null)
        {
            return new ApiResponse<T> { TimeStamp = DateTime.UtcNow,Success = true,Data = data, Message = message };
        }
        public static ApiResponse<T> Fail(string message, object? errors = null)
        {
            return new ApiResponse<T> { TimeStamp = DateTime.UtcNow, Success = false, Message = message, Errors = errors };
        }
    }
}
