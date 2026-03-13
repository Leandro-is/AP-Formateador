using IS.DocumenFormater.api.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;

namespace IS.DocumenFormater.api.Policy
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class ApiAuthorization : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var serviceProvider = context.HttpContext.RequestServices;
            IConfiguration configuration = (IConfiguration)serviceProvider.GetService(typeof(IConfiguration));

            var request = context.HttpContext.Request;
            string token = request.Headers["t"];

            String currentToken = configuration.GetValue<String>("Token");

            //lastToke = "fM%6NC^Ax%xD4Mnmq4S$WBV=!3Yr^$cd";
            if (String.IsNullOrEmpty(token) || !token.Equals(currentToken))
                context.Result = new JsonResult(GenericResponse.CreateInstance(GenericResponse.ResponseGenericCode.Unauthorized));
        }
    }
}
