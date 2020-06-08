using System;
using BludataTest.CustomExceptions;
using BludataTest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BludataTest.Filter
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            JsonResult result;
            if (context.Exception.GetType() == typeof(ValidationException))
            {
                result = new JsonResult(new
                {
                    message = context.Exception.Message,
                });
                result.StatusCode = 400;
            }
            else
            {
                result = new JsonResult(new
                {
                    message = "Ocorreu um erro no servidor. Contate administradores",
                });
                result.StatusCode = 500;

                LogException(context.Exception);
            }

            context.Result = result;
        }

        private void LogException(Exception exception)
        {
            var logger = new Logger();
            var messageToLog = exception.Message + "\r\n" + exception.StackTrace;
            logger.RegisterLogInFile(messageToLog);
        }
    }
}