namespace WebApi.Utils;

public class ApiResponse
{
    public bool IsSuccess { get; set; } = false;
    public string? ErrorMessage { get; set; }
    public object? Result { get; set; }
}
