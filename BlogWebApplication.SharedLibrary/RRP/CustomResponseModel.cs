using BlogWebApplication.SharedLibrary.Enums;

namespace BlogWebApplication.SharedLibrary.RRP
{
    public class CustomResponseModel<T> where T : class
    {
        public T? Data { get; set; }
        public ResponseType ResponseType { get; set; }
        public List<string>? Errors { get; set; }
        public List<CustomValidationError>? ValidationErrors { get; set; }
        public List<CustomIdentityError>? IdentityErrors { get; set; }
        public string? isSuccessMessage { get; set; }


        public static CustomResponseModel<T> Success(ResponseType responseType, T data, string successMessage)
        {
            return new CustomResponseModel<T>()
            {
                ResponseType = responseType,
                Data = data,
                isSuccessMessage = successMessage
            };
        }

        public static CustomResponseModel<T> Success(ResponseType responseType, string successMessage)
        {
            return new CustomResponseModel<T>()
            {
                ResponseType = responseType,
                isSuccessMessage = successMessage
            };
        }

        public static CustomResponseModel<T> Fail(ResponseType responseType, string errorMessage)
        {
            return new CustomResponseModel<T>()
            {
                ResponseType = responseType,
                Errors = new() { errorMessage }
            };
        }

        public static CustomResponseModel<T> Fail(ResponseType responseType, List<string> errorMessages)
        {
            return new CustomResponseModel<T>()
            {
                ResponseType = responseType,
                Errors = errorMessages
            };
        }

        public static CustomResponseModel<T> ValidationFail(ResponseType responseType, CustomValidationError validationError)
        {
            return new CustomResponseModel<T>()
            {
                ResponseType = responseType,
                ValidationErrors = new() { validationError }
            };
        }

        public static CustomResponseModel<T> ValidationFail(ResponseType responseType, List<CustomValidationError> validationErrors)
        {
            return new CustomResponseModel<T>()
            {
                ResponseType = responseType,
                ValidationErrors = validationErrors
            };
        }

        public static CustomResponseModel<T> ValidationUpdateFail(ResponseType responseType, T data, CustomValidationError validationError)
        {
            return new CustomResponseModel<T>()
            {
                ResponseType = responseType,
                Data = data,
                ValidationErrors = new() { validationError }
            };

        }

        public static CustomResponseModel<T> ValidationUpdateFail(ResponseType responseType, T data, List<CustomValidationError> validationErrors)
        {
            return new CustomResponseModel<T>()
            {
                ResponseType = responseType,
                Data = data,
                ValidationErrors = validationErrors
            };

        }

        public static CustomResponseModel<T> IdentityFail(ResponseType responseType, CustomIdentityError identityError)
        {
            return new CustomResponseModel<T>()
            {
                ResponseType = responseType,
                IdentityErrors = new() { identityError }
            };
        }

        public static CustomResponseModel<T> IdentityFail(ResponseType responseType, List<CustomIdentityError> identityErrors)
        {
            return new CustomResponseModel<T>()
            {
                ResponseType = responseType,
                IdentityErrors = identityErrors
            };


        }

    }
}
