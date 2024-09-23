namespace Inlämning_1.Models;

public class ResultResponse<T> where T : class
{
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public T? Result { get; set; }
}
