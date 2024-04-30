namespace Tokobaju.Dto;

public class ErrorDto
{
    public required int StatusCode { get; set; }
    public required string Message { get; set; }
}