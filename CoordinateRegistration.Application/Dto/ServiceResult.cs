using FluentValidation.Results;
using System.Net;

namespace CoordinateRegistration.Application.Dto
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public List<string> Message { get; set; }
        public T? Data { get; set; }
        public int StatusCode { get; set; }


        public static ServiceResult<T> SucessResult(T data, int statusCode)
        {
            return new ServiceResult<T> { Success = true, Data = data, Message = new List<string> {"Operação concluída com sucesso."}, StatusCode = statusCode };
        }

        public static ServiceResult<T> SucessResult(int statusCode)
        {
            return new ServiceResult<T> { Success = true, Message = new List<string> {"Operação concluída com sucesso."}, StatusCode = statusCode };
        }

        public static ServiceResult<T> FailResult(int statusCode)
        {
            return new ServiceResult<T> { Success = false, Message = new List<String> { "Ocorreu um erro inesperado. Não foi possível concluir a operação." }, StatusCode = statusCode };

        }

        public static ServiceResult<T> FailResult(string message, int statusCode)
        {
            return new ServiceResult<T> { Success = false, Message = new List<String> {"Não foi possível concluir a operação.", message}, StatusCode = statusCode };

        }

        public static ServiceResult<T> FailResult(ValidationResult message, int statusCode)
        {
            var errors = new List<string>();
            errors.Add("Não foi possível concluir a operação.");

            foreach (var item in message.Errors)
            {
                errors.Add(item.ErrorMessage);
            }

            return new ServiceResult<T> { Success = false, Message = errors, StatusCode = statusCode };

        }
        //to do
        public static ServiceResult<T> FailResult(List<ValidationResult> message, int statusCode)
        {
            var errors = new List<ValidationResult>();

    
            return new ServiceResult<T> { Success = false, Message = {"Tratar error" } , StatusCode = statusCode };

        }

    }
}
