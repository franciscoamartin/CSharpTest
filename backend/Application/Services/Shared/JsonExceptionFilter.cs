using BludataTest.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BludataTest.Filter
{
    public class JsonExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            ObjectResult result;
            if (context.Exception.GetType() == typeof(ValidationException))
            {
                result = new ObjectResult(new
                {
                    code = 500,
                    message = context.Exception.Message,
                });
            }
            else
            {
                result = new ObjectResult(new
                {
                    code = 500,
                    message = "Ocorreu um erro no servidor. Contate administradores",
                });
            }

            result.StatusCode = 500;
            context.Result = result;
        }
    }
}