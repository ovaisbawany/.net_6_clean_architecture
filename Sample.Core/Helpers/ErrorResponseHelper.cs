using Sample.Core.DTOBase;

namespace Sample.Core.Helpers
{
    public static class ErrorResponseHelper
    {
        public static DataTransferObject<T> CreateErrorResponse<T>(List<string> errors)
        {
            var errorResponse = new DataTransferObject<T>();
            errorResponse.HasError = true;
            errorResponse.Errors = errors;
            return errorResponse;
        }

        public static DataTransferObject<T> CreateErrorResponse<T>()
        {
            var errorResponse = new DataTransferObject<T>();
            errorResponse.HasError = true;
            errorResponse.Errors = new List<string>();
            return errorResponse;
        }
    }
}
