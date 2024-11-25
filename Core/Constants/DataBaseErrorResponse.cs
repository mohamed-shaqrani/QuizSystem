using Core.Enum;
using Core.ViewModels;

namespace Core.Constants;
public static class DataBaseError
{
    public static ResponseViewModel<int> DataBaseErrorResponse(ResponseViewModel<int> response)
    {
        response.ErrorCode = ErrorCode.DatabaseSaveError;
        response.IsSuccess = false;
        response.Message = "something went wrong";
        return response;
    }

}
