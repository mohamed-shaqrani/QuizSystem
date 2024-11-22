using Core.Enum;

namespace Core.ViewModels;
public class ResponseViewModel<T>
{
    public T Data { get; set; }
    public bool IsSuccess {  get; set; }
    public ErrorCode ErrorCode { get; set; }
    public string Message { get; set; }
}
