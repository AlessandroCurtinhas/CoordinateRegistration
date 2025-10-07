using FluentValidation.Results;
using System.Net;

namespace CoordinateRegistration.Application.Dto
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public List<string> Message { get; set; }
        public T? Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public static ServiceResult<T> SucessResult(T data)
        {
            return new ServiceResult<T> { Success = true, Data = data, Message = new List<string> {"Operação concluída com sucesso."} };
        }

        public static ServiceResult<T> SucessResult()
        {
            return new ServiceResult<T> { Success = true, Message = new List<string> {"Operação concluída com sucesso."} };
        }

        public static ServiceResult<T> FailResult()
        {
            return new ServiceResult<T> { Success = false, Message = new List<String> { "Ocorreu um erro inesperado. Não foi possível concluir a operação." } };

        }

        public static ServiceResult<T> FailResult(string message)
        {
            return new ServiceResult<T> { Success = false, Message = new List<String> {"Não foi possível concluir a operação.", message} };

        }

        public static ServiceResult<T> FailResult(ValidationResult message)
        {
            var errors = new List<string>();
            errors.Add("Não foi possível concluir a operação.");

            foreach (var item in message.Errors)
            {
                errors.Add(item.ErrorMessage);
            }

            return new ServiceResult<T> { Success = false, Message = errors };

        }
        //to do
        public static ServiceResult<T> FailResult(List<ValidationResult> message)
        {
            var errors = new List<ValidationResult>();

    
            return new ServiceResult<T> { Success = false, Message = {"Tratar error" } };

        }

    }
}
