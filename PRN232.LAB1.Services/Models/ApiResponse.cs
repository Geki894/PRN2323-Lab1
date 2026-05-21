using System.Collections.Generic;

namespace PRN232.LAB1.Services.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public PaginationMetadata? Pagination { get; set; }
        public List<string>? Errors { get; set; }

        public static ApiResponse<T> SuccessResult(T data, string message = "Operation successful", PaginationMetadata? pagination = null)
        {
            return new ApiResponse<T> { Success = true, Message = message, Data = data, Pagination = pagination };
        }

        public static ApiResponse<T> ErrorResult(string message, List<string>? errors = null)
        {
            return new ApiResponse<T> { Success = false, Message = message, Errors = errors };
        }
    }
}
