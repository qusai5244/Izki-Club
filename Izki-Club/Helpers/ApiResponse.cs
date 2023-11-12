public class ApiResponse<T>
{
    public bool Status { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }

    public ApiResponse(bool status, int statusCode, string message, T data)
    {
        Status = status;
        StatusCode = statusCode;
        Message = message;
        Data = data;
    }
}
